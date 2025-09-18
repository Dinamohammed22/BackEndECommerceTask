using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;

namespace KOG.ECommerce.Features.Orders.CompletedOrder.Commands
{
    public record CompletedOrderCommand(string ID):IRequestBase<bool>;
    public class CompletedOrderCommandHandler : RequestHandlerBase<Order, CompletedOrderCommand, bool>
    {
        public CompletedOrderCommandHandler(RequestHandlerBaseParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(CompletedOrderCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(p => p.ID == request.ID);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.OrderNotFound);
            Order order = new Order { ID = request.ID };
            order.Status = OrderStatus.Completed;
            order.CompletedDate = DateTime.Now;
            _repository.SaveIncluded(order, nameof(order.Status), nameof(order.CompletedDate));
            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }
    }
}
