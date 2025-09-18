using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Data;
using KOG.ECommerce.Features.Common.Users.Queries;
using KOG.ECommerce.Features.NotificationMessages.SendNotification.Commands;
using KOG.ECommerce.Features.Orders.ApproveOrder.Commands;
using KOG.ECommerce.Features.Products.DecreaseProductQuantity.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Orders.ApproveOrder.Orchistrator
{
    public record ApproveOrderOrchistrator(string ID) : IRequestBase<bool>;
    public class ApproveOrderOrchistratorHandler : RequestHandlerBase<Order, ApproveOrderOrchistrator, bool>
    {
        public ApproveOrderOrchistratorHandler(RequestHandlerBaseParameters<Order> requestParameters) : base(requestParameters)
        {
        }
        public async override Task<RequestResult<bool>> Handle(ApproveOrderOrchistrator request, CancellationToken cancellationToken)
        {
            var ApproveCheck = await _mediator.Send(new ApproveOrderCommand(request.ID));
            if (!ApproveCheck.Data)
            {
                return RequestResult<bool>.Failure(ErrorCode.ProductNotApproved);
            }

            var order = await _repository.Get(o => o.ID == request.ID)
                .Include(o => o.OrderItems)
                .Include(o=>o.Client)
                .FirstOrDefaultAsync(cancellationToken);

            foreach (var orderItem in order.OrderItems)
            {
                var decreaseQuantityResult = await _mediator.Send(
                    new DecreaseProductQuantityCommand(orderItem.ProductId, orderItem.Quantity)
                );

                if (!decreaseQuantityResult.IsSuccess)
                {
                    return RequestResult<bool>.Failure(decreaseQuantityResult.ErrorCode);
                }
            }
            var userId=_repository.GetByID(request.ID).ClientId;
            var ToFirebaseToken = await _mediator.Send(new GetFirebaseTokenByUserIDQuery(userId));
            if (!ToFirebaseToken.IsSuccess)
            {
                return RequestResult<bool>.Failure(ToFirebaseToken.ErrorCode);
            }
            var ToSendNotification = await _mediator.Send(new SendNotificationCommand(userId, "لفد تم تأكيد طلبك بنجاح",
                       "نشكر لك ثقتك بنا. لقد تم تأكيد طلبك وجارٍ العمل على تنفيذه. لمزيد من التفاصيل، يمكنك متابعة حالة الطلب من خلال التطبيق.",
                    ToFirebaseToken.Data));
            if (!ToSendNotification.IsSuccess)
            {
                return RequestResult<bool>.Failure(ToSendNotification.ErrorCode);
            }
            //string Message = "لفد تم تأكيد طلبك بنجاح";
            //var smsResponse = await SMSHelper.SendSmsAsync(order.Client.Mobile, Message);

            return RequestResult<bool>.Success(true);
        }
    }
}
