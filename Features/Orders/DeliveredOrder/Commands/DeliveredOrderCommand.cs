using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;

namespace KOG.ECommerce.Features.Orders.DeliveredOrder.Commands
{
    public record DeliveredOrderCommand(string ID) : IRequestBase<bool>;
    public class DeliveredOrderCommandHandler : RequestHandlerBase<Order, DeliveredOrderCommand, bool>
    {
        public DeliveredOrderCommandHandler(RequestHandlerBaseParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(DeliveredOrderCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(p => p.ID == request.ID);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.OrderNotFound);
            Order order = new Order { ID = request.ID };
            order.Status = OrderStatus.Delivered;
            order.DeliveryDate = DateTime.Now;
            _repository.SaveIncluded(order, nameof(order.Status), nameof(order.DeliveryDate));
            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }
    }
}
