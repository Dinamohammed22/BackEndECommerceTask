using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Features.Orders.PlacedAnOrder.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.ShippingAddresses;
using Microsoft.IdentityModel.Tokens;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Models.OrderItems;
using KOG.ECommerce.Features.OrderItems.AddOrderItem.Commands;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Features.Orders.PlaceAnOrder;
using KOG.ECommerce.Features.Common.CartProducts.DTOs;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Features.Common.Discounts.Queries;
using KOG.ECommerce.Features.Common.ShippingAddresses.Commands;
using Microsoft.EntityFrameworkCore;
using KOG.ECommerce.Features.Common.OrderItems.DTOs;
using KOG.ECommerce.Features.Common.Orders.Queries;
using KOG.ECommerce.Features.Common.Companies.Queries;

namespace KOG.ECommerce.Features.Orders.PlacedAnOrder.Orchestrators
{
    public record PlaceOrderOrchestrator(
        OrderStatus OrderStatus,
        string ClientID,
        string? Comment, 
        string? ShippingAddressId,
        string? GovernorateId,
        string? CityId,
        string? Street, 
        string? Landmark,
        double? Latitude,
        double? Longitude,
        bool? IsDefualt,
        IEnumerable<GetAllProductAtCartDTO> cartProductsResult,
        string? BuildingData,
        ShippingAddressStatus? Status
    ) : IRequestBase<string>;

    public class PlaceOrderOrchestratorHandler : RequestHandlerBase<ShippingAddress, PlaceOrderOrchestrator, string>
    {
        public PlaceOrderOrchestratorHandler(RequestHandlerBaseParameters<ShippingAddress> requestParameters) : base(requestParameters)
        {
        }
        public override async Task<RequestResult<string>> Handle(PlaceOrderOrchestrator request, CancellationToken cancellationToken)
        {
            // Step 1: Group cart items by CompanyId
            var groupedByCompany = request.cartProductsResult
                .GroupBy(p => p.CompanyId)
                .Select(g => new
                {
                    CompanyId = g.Key,
                    TotalQuantity = g.Sum(p => p.Quantity)
                })
                .ToList();

            foreach (var g in groupedByCompany)
            {
                var check = await _mediator.Send(new CheckCompanyMinimumQuantityQuery(g.CompanyId ,g.TotalQuantity));
                if (!check.IsSuccess)
                {
                   return RequestResult<string>.Failure(check.ErrorCode, check.Message);
                }
            }

            //1.Shiiping address
            var shippingAddressId = request.ShippingAddressId;
            var shipping = _repository.Any(c => c.ID == request.ShippingAddressId);

            if (!shipping && string.IsNullOrEmpty(request.GovernorateId))
            {
                return RequestResult<string>.Failure(ErrorCode.NotFound);
            }

            if (shippingAddressId.IsNullOrEmpty())
            {
                var shippingAddress = await _mediator.Send(request.MapOne<CreateShippingAddressInOrderCommand>());
                return RequestResult<string>.Failure(shippingAddress.ErrorCode);
                shippingAddressId = shippingAddress.Data;
            }

            //2.get products 
            //var productsWithQuantities = request.cartProductsResult
            //    .Select(p => new GetAllProductAtCartDTO(p.ProductId, p.CompanyId , p.Quantity,p.MinimumQuantity,p.MaximumQuantity))
            //    .ToList();

            var getPriceOfProductsResult = await _mediator.Send(new GetPriceOfProductsQuery(request.cartProductsResult.ToList()));
            if (getPriceOfProductsResult.Data == null || !getPriceOfProductsResult.Data.Any())
            {
                return RequestResult<string>.Failure(ErrorCode.ProductNotFound);
            }

            //3.calculate discount 

            var currentDiscount = await _mediator.Send(new CheckCurrentDiscountQuery());
            // Prepare items for the discount calculation query
            var items = getPriceOfProductsResult.Data
                .Select(p => new EditOrderDTO(
                    ProductId: p.ProductId,
                    Quantity: p.Quantity,
                    ItemPrice: p.Price
                )).ToList();


            var discountCalculationResult = await _mediator.Send(new CalculateDiscountQuery(items, currentDiscount?.Data));

            //4.Total weight
            var TotalLiter = await _mediator.Send(new CalculateProductsTotalLiterQuery(
                getPriceOfProductsResult.Data
                .Select(p => new ProductIDandQuantityDTO(
                    ProductId: p.ProductId,
                    Quantity: p.Quantity
                )).ToList() 
            ));

            //5.Total Points
            var TotalPoints = await _mediator.Send(new CalculateProductsTotalPointsQuery(
               getPriceOfProductsResult.Data
               .Select(p => new ProductIDandQuantityDTO(
                   ProductId: p.ProductId,
                   Quantity: p.Quantity
               )).ToList()
           ));


            var placeOrderDto = new PlaceOrderDto(
                ClientID: request.ClientID,
                Comment: request.Comment,
                ShippingAddressId: shippingAddressId,
                Products: getPriceOfProductsResult.Data,
                DiscountId: currentDiscount?.Data?.ID,
                CurrentDiscount: currentDiscount?.Data,
                TotalPrice: discountCalculationResult.Data.TotalPrice,
                TotalnetPrice: discountCalculationResult.Data.TotalNetPrice,
                TotalQuantity: getPriceOfProductsResult.Data.Sum(item => item.Quantity)
            );

            var order = await _mediator.Send(new PlaceAnOrderCommand(
                OrderStatus: OrderStatus.Pending,
                ClientID: placeOrderDto.ClientID,
                Comment: placeOrderDto.Comment ?? "",
                ShippingAddressId: placeOrderDto.ShippingAddressId ?? "",
                Products: placeOrderDto.Products,
                DiscountId: placeOrderDto.DiscountId,
                CurrentDiscount: placeOrderDto.CurrentDiscount,
                TotalPrice: placeOrderDto.TotalPrice,
                TotalnetPrice: placeOrderDto.TotalnetPrice,
                TotalQuantity: placeOrderDto.TotalQuantity,
                TotalLiter: TotalLiter.Data,
                TotalPoints: TotalPoints.Data,
                TotalDiscountAmount: discountCalculationResult.Data.ItemsDiscountAmount.FirstOrDefault()
            ));

            var mappedItems = getPriceOfProductsResult.Data
                .Select((item, index) => CreateOrderItem(
                    item, 
                    order.Data.ID,
                    discountCalculationResult.Data.ItemsNetPrice.ElementAtOrDefault(index),
                    discountCalculationResult.Data.ItemsDiscountAmount.ElementAtOrDefault(index))
                    )
                .ToList();

            var addResult = await _mediator.Send(new AddOrderItemsCommand(mappedItems));

            // Send notification message
            var mobile = await _repository
                .Get(sa => sa.ID == shippingAddressId)
                .Select(sa => sa.Client.Mobile)
                .FirstOrDefaultAsync();

            //string message = "لقد تم استلام طلبك بنجاح" +
            //    "Your order has been received successfully";
            //var smsResponse = await SMSHelper.SendSmsAsync(mobile, message);

            return RequestResult<string>.Success(order.Data.OrderNumber);
        }

        private OrderItem CreateOrderItem(GetPriceOfProductsDTO item, string orderId, double netPrice, double discountAmount)
        {
            return new OrderItem
            {
                OrderId = orderId,
                ProductId = item.ProductId,
                ItemPrice = item.Price,
                Quantity = item.Quantity,
                Price = item.Price * item.Quantity,
                DiscountAmount = discountAmount,
                NetPrice = netPrice,
                ItemLiter = item.Liter,
                Liter = item.Liter * item.Quantity,
                ItemPoint = item.Point,
                Point = item.Point * item.Quantity,
            };
        }

    }
}
