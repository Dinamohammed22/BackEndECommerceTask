using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Users.Queries;
using KOG.ECommerce.Features.NotificationMessages.SendNotification.Commands;
using KOG.ECommerce.Features.Orders.DeliveredOrder.Commands;
using KOG.ECommerce.Models.Orders;

namespace KOG.ECommerce.Features.Orders.DeliveredOrder.Orchestrators
{
    public record DeliveredOrderOrchestrator(string ID):IRequestBase<bool>;
    public class DeliveredOrderOrchestratorHandler : RequestHandlerBase<Order, DeliveredOrderOrchestrator, bool>
    {
        public DeliveredOrderOrchestratorHandler(RequestHandlerBaseParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(DeliveredOrderOrchestrator request, CancellationToken cancellationToken)
        {
            var commandResult = await _mediator.Send(new DeliveredOrderCommand(request.ID));
            if (!commandResult.IsSuccess)
            {
                return RequestResult<bool>.Failure(commandResult.ErrorCode);
            }
            var clientId = _repository.GetByID(request.ID).ClientId;
            var ToFirebaseToken = await _mediator.Send(new GetFirebaseTokenByUserIDQuery(clientId));
            if (!ToFirebaseToken.IsSuccess)
            {
                return RequestResult<bool>.Failure(ToFirebaseToken.ErrorCode);
            }
            var ToSendNotification = await _mediator.Send(new SendNotificationCommand(clientId, "تم تسليم طلبك بنجاح",
                    ".لقد تم تسليم طلبك بنجاح. نأمل أن تكون تجربتك مرضية! شكرًا لاختيارك خدمتنا، ونتمنى رؤيتك مجددًا قريبًا ",
                    ToFirebaseToken.Data));
            if (!ToSendNotification.IsSuccess)
            {
                return RequestResult<bool>.Failure(ToSendNotification.ErrorCode);
            }
            return RequestResult<bool>.Success(true);
        }
    }
}
