using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;

namespace KOG.ECommerce.Features.Orders.InProcessOrder.Commands
{
    public record InProcessOrderCommand(string ID) : IRequestBase<bool>;
    public class InProcessOrderCommandHandler : RequestHandlerBase<Order, InProcessOrderCommand, bool>
    {
        public InProcessOrderCommandHandler(RequestHandlerBaseParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(InProcessOrderCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(p => p.ID == request.ID);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.OrderNotFound);
            Order order = new Order { ID = request.ID };
            order.Status = OrderStatus.InProcess;
            order.InProcessDate = DateTime.Now;
            _repository.SaveIncluded(order, nameof(order.Status), nameof(order.InProcessDate));
            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }
    }
}
