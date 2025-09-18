using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;
using KOG.ECommerce.Models.ShippingAddresses;
using MediatR.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Orders.CancelOrder.Commands
{
    public record CancelOrderCommand(string ID):IRequestBase<bool>;
    public class CancelOrderCommandHandler : RequestHandlerBase<Order, CancelOrderCommand, bool>
    {
        public CancelOrderCommandHandler(RequestHandlerBaseParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var check = _repository.Any(o => o.ID == request.ID);
            if (check)
            {
                Order order = new Order {ID = request.ID};
                order.Status = OrderStatus.Cancelled;
                order.CancellationDate = DateTime.Now;
                _repository.SaveIncluded(order, nameof(order.Status), nameof(order.CancellationDate));
                _repository.SaveChanges();

                //send message
                var mobile = await _repository
                    .Get(o => o.ID == request.ID)
                    .Select(o => o.Client.Mobile)
                    .FirstOrDefaultAsync();
               
                //string Message = "لفد تم الغاء طلبك";
                //var smsResponse = await SMSHelper.SendSmsAsync(mobile, Message);

                return RequestResult<bool>.Success(true);
            }
            else
            {
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            }

        }
    }
}
