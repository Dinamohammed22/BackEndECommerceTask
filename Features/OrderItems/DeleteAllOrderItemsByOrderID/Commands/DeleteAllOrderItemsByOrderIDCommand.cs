using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Data;
using KOG.ECommerce.Models.OrderItems;

namespace KOG.ECommerce.Features.OrderItems.EditOrderItem.Commands
{
    public record DeleteAllOrderItemsByOrderIDCommand(string OrderID) : IRequestBase<bool>;

    public class AddOrderItemCommandHandler : RequestHandlerBase<OrderItem, DeleteAllOrderItemsByOrderIDCommand, bool>
    {
        public AddOrderItemCommandHandler(RequestHandlerBaseParameters<OrderItem> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(DeleteAllOrderItemsByOrderIDCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.OrderID))
            {
                return RequestResult<bool>.Failure(ErrorCode.OrderNotFound);
            }

            var orderItems = _repository.Get(item => item.OrderId == request.OrderID).ToList();

            foreach (var orderItem in orderItems)
            {
                _repository.Delete(orderItem);
            }

            _repository.SaveChanges();

            return RequestResult<bool>.Success(true);

        }
    }
}
