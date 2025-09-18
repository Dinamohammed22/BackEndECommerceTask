using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;

namespace KOG.ECommerce.Features.Orders.ApproveOrder.Commands
{
    public record ApproveOrderCommand(string ID):IRequestBase<bool>;
    public class ApproveOrderCommandHandler : RequestHandlerBase<Order, ApproveOrderCommand, bool>
    {
        public ApproveOrderCommandHandler(RequestHandlerBaseParameters<Order> requestParameters) : base(requestParameters)
        {
        }
        public async override Task<RequestResult<bool>> Handle(ApproveOrderCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(p => p.ID == request.ID);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.OrderNotFound);
            Order order = new Order {ID = request.ID };
            order.Status = OrderStatus.Confirmed;
            order.ConfirmationDate = DateTime.Now;   
            _repository.SaveIncluded(order, nameof(order.Status),nameof(order.ConfirmationDate));
            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }
    }

}
