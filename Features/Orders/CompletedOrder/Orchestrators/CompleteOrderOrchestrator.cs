using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Users.Queries;
using KOG.ECommerce.Features.NotificationMessages.SendNotification.Commands;
using KOG.ECommerce.Features.Orders.CompletedOrder.Commands;
using KOG.ECommerce.Models.Orders;

namespace KOG.ECommerce.Features.Orders.CompletedOrder.Orchestrators
{
    public record CompleteOrderOrchestrator(string ID) : IRequestBase<bool>;
    public class CompleteOrderOrchestratorHandler : RequestHandlerBase<Order, CompleteOrderOrchestrator, bool>
    {
        public CompleteOrderOrchestratorHandler(RequestHandlerBaseParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(CompleteOrderOrchestrator request, CancellationToken cancellationToken)
        {
            var commandResult = await _mediator.Send(new CompletedOrderCommand(request.ID));
            if (!commandResult.IsSuccess)
            {
                return RequestResult<bool>.Failure(commandResult.ErrorCode);
            }
            var clientId =  _repository.GetByID(request.ID).ClientId;
            var ToFirebaseToken = await _mediator.Send(new GetFirebaseTokenByUserIDQuery(clientId));
            if (!ToFirebaseToken.IsSuccess)
            {
                return RequestResult<bool>.Failure(ToFirebaseToken.ErrorCode);
            }
            var ToSendNotification = await _mediator.Send(new SendNotificationCommand(clientId, "طلبك فى الطريق إليك",
                    "! سائق التوصيل في طريقه إليك الآن. يرجى التأكد من وجودك في الموقع المحدد واستعدادك لاستلام الطلب. شكرًالاختيارك خدمتنا ",
                    ToFirebaseToken.Data));
            if (!ToSendNotification.IsSuccess)
            {
                return RequestResult<bool>.Failure(ToSendNotification.ErrorCode);
            }
            return RequestResult<bool>.Success(true);
        }
    }
}
