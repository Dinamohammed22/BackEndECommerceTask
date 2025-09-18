using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Discounts.DTOs;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;

namespace KOG.ECommerce.Features.Orders.PlacedAnOrder.Commands
{
    public record PlaceAnOrderCommand(
    OrderStatus OrderStatus,
    string ClientID,
     double TotalLiter,
    string Comment = "",
    string ShippingAddressId = "",
    IEnumerable<GetPriceOfProductsDTO> Products=null ,
    string? DiscountId=null ,
    GetAllDiscountsDTO? CurrentDiscount=null,
     double TotalPrice=0,
     double TotalnetPrice = 0,
     int TotalQuantity=0,
     int TotalPoints=0,
     double TotalDiscountAmount=0
) : IRequestBase<PlaceOrderDTO>;

    public class PlacedAnOrderCommandHandler : RequestHandlerBase<Order, PlaceAnOrderCommand, PlaceOrderDTO>
    {
        public PlacedAnOrderCommandHandler(RequestHandlerBaseParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<PlaceOrderDTO>> Handle(PlaceAnOrderCommand request, CancellationToken cancellationToken)
        {
            double totalDiscountAmount = 0;
            if (request.CurrentDiscount != null)
            {
                totalDiscountAmount = request.TotalDiscountAmount;
            }
            string OrderNumber = GenerateUniqueNumber();   
            var order = new Order
            {
                TotalPrice = request.TotalPrice,
                TotalNetPrice = request.TotalnetPrice,
                TotalQuantity = request.TotalQuantity,
                Status = request.OrderStatus,
                DiscountAmount = totalDiscountAmount,
                DiscountId = request.DiscountId,
                ShippingAddressId = request.ShippingAddressId,
                ClientId =request.ClientID,
                Comment = request.Comment,
                OrderNumber = OrderNumber,
                TotalLiter = request.TotalLiter,
                TotalNumberOfPoints = request.TotalPoints,
            };

            _repository.Add(order);
            _repository.SaveChanges();
            PlaceOrderDTO orderDTO=new PlaceOrderDTO (ID: order.ID , OrderNumber:order.OrderNumber);
            return RequestResult<PlaceOrderDTO>.Success(orderDTO);
        }
        public static string GenerateUniqueNumber()
        {
            Random _random = new Random();
            var timestampPart = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString().Substring(0, 9);
            var randomPart = _random.Next(10000, 999999999).ToString();
            return timestampPart + randomPart;
        }

    }
}
