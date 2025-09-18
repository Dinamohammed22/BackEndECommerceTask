using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Users.Queries;
using KOG.ECommerce.Features.NotificationMessages.SendNotification.Commands;
using KOG.ECommerce.Models.Notifications;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Features.NotificationMessages.SendNotification.Orchestrator
{
    public record SendNotificationOrchestrator(List<string> UserId, string Title, string Body) : IRequestBase<bool>;
    public class SendNotificationOrchestratorHandler : RequestHandlerBase<NotificationMessage, SendNotificationOrchestrator, bool>
    {
        public SendNotificationOrchestratorHandler(RequestHandlerBaseParameters<NotificationMessage> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(SendNotificationOrchestrator request, CancellationToken cancellationToken)
        {
            foreach (var userId in request.UserId)
            {
                var FirebaseToken = await _mediator.Send(new GetFirebaseTokenByUserIDQuery(userId));
                if (!FirebaseToken.IsSuccess)
                {
                    return RequestResult<bool>.Failure(FirebaseToken.ErrorCode,FirebaseToken.Message);
                }
                var command = new SendNotificationCommand(
                    UserId: userId,
                    Title: request.Title,
                    Body: request.Body,
                    Token: FirebaseToken.Data
                );

                var result = await _mediator.Send(command, cancellationToken);
                if (!result.IsSuccess)
                {
                    return RequestResult<bool>.Failure(result.ErrorCode);
                }
            }

            return RequestResult<bool>.Success(true);
        }
    }
}
