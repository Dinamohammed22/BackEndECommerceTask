using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.OrderItems.EditOrderItem.Commands;
using KOG.ECommerce.Features.OrderItems.AddOrderItem.Commands;
using KOG.ECommerce.Models.Orders;
using KOG.ECommerce.Models.OrderItems;
using KOG.ECommerce.Models.Enums;
using Microsoft.EntityFrameworkCore;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Features.Common.Discounts.Queries;
using KOG.ECommerce.Features.Common.OrderItems.DTOs;
using KOG.ECommerce.Models.Discounts;
using KOG.ECommerce.Features.Common.Discounts.DTOs;
using KOG.ECommerce.Features.Common.Orders.Queries;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Migrations;

namespace KOG.ECommerce.Features.Orders.EditOrder.Commands
{
    public record EditOrderCommand(
        string OrderID,
        string OrderNumber,
        List<EditOrderDTO> Items,
        OrderStatus Status,
        string? Comment,
        string ShippingAddressID
    ) : IRequestBase<bool>;

    public class EditOrderCommandHandler : RequestHandlerBase<Order, EditOrderCommand, bool>
    {
        public EditOrderCommandHandler(RequestHandlerBaseParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(EditOrderCommand request, CancellationToken cancellationToken)
        {
            var existingOrder = await _repository
                .Get(o => o.ID == request.OrderID)
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync();

            if (existingOrder == null)
            {
                return RequestResult<bool>.Failure(ErrorCode.OrderNotFound);
            }

            var currentDiscount = await _mediator.Send(new GetDiscountByIDQuery(existingOrder.DiscountId));
            var discountResult = await _mediator.Send(new CalculateDiscountQuery(request.Items, currentDiscount?.Data));

            //order item update
            if (existingOrder.OrderItems.Any())
            {
                var deleteResult = await _mediator.Send(new DeleteAllOrderItemsByOrderIDCommand(request.OrderID));
                existingOrder.OrderItems = null;
            }

            //calculate total weight
            double TotalLiter = 0;
            List<double> ProductsLiters = new List<double>();

            foreach (var product in request.Items)
            {
                var ProductLiter = _mediator.Send(new GetProductLiterQuery(product.ProductId));
                ProductsLiters.Add(ProductLiter.Result.Data);

                TotalLiter += ProductLiter.Result.Data * product.Quantity;
            }

            var mappedItems = request.Items
                .Select((item, index) => CreateOrderItem(
                    item, request.OrderID,
                    discountResult.Data.ItemsNetPrice.ElementAtOrDefault(index), discountResult.Data.ItemsDiscountAmount.ElementAtOrDefault(index),
                    ProductsLiters.ElementAtOrDefault(index))
                )
                .ToList();

            var addResult = await _mediator.Send(new AddOrderItemsCommand(mappedItems));

            if (!addResult.Data)
            {
                return RequestResult<bool>.Failure(ErrorCode.UnKnown);
            }


            UpdateOrder(existingOrder, request, discountResult.Data.TotalPrice, discountResult.Data.TotalNetPrice, currentDiscount?.Data, TotalLiter);

            _repository.Update(existingOrder);
            _repository.SaveChanges();

            return RequestResult<bool>.Success(true);
        }

        private OrderItem CreateOrderItem(EditOrderDTO item, string orderId, double netPrice, double discountAmount ,double ItemLiter)
        {
            return new OrderItem
            {
                OrderId = orderId,
                ProductId = item.ProductId,
                ItemPrice = item.ItemPrice,
                Quantity = item.Quantity,
                Price = item.ItemPrice * item.Quantity,
                DiscountAmount = discountAmount,
                NetPrice = netPrice,
                ItemLiter = ItemLiter,
                Liter = ItemLiter * item.Quantity
            };
        }

        private void UpdateOrder(Order existingOrder, EditOrderCommand request, double totalPrice, double totalNetPrice, GetAllDiscountsDTO? currentDiscount ,double totalLiter)
        {
            existingOrder.OrderNumber = request.OrderNumber;
            existingOrder.Status = request.Status;
            existingOrder.Comment = request.Comment;
            existingOrder.TotalPrice = totalPrice;
            existingOrder.TotalNetPrice = totalNetPrice;
            existingOrder.TotalQuantity = request.Items.Sum(item => item.Quantity);
            existingOrder.DiscountId = currentDiscount?.ID;
            existingOrder.DiscountAmount = currentDiscount?.Amount ?? 0;
            existingOrder.ShippingAddressId = request.ShippingAddressID;
            existingOrder.TotalLiter = totalLiter;
        }

    }
}
