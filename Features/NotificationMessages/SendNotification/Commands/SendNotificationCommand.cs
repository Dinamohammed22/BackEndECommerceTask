using FirebaseAdmin.Messaging;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Notifications;
using KOG.ECommerce.Common.Enums;

namespace KOG.ECommerce.Features.NotificationMessages.SendNotification.Commands
{
    public record SendNotificationCommand(string UserId, string Title, string Body, string Token) : IRequestBase<bool>;

    public class SendNotificationCommandHandler : RequestHandlerBase<NotificationMessage, SendNotificationCommand, bool>
    {
        public SendNotificationCommandHandler(RequestHandlerBaseParameters<NotificationMessage> requestParameters)
            : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var notificationMessage = new NotificationMessage
                {
                    Body = request.Body,
                    Token = request.Token,
                    UserId = request.UserId,
                    Title = request.Title
                };

                _repository.Add(notificationMessage);
                _repository.SaveChanges();
                var message = new Message()
                {
                    Token = request.Token,
                    Notification = new Notification
                    {
                        Title = request.Title,
                        Body = request.Body
                    }
                };

                string response = await FirebaseMessaging.DefaultInstance.SendAsync(message, cancellationToken);

                if (string.IsNullOrWhiteSpace(response))
                {
                    return RequestResult<bool>.Failure(ErrorCode.NotFound);
                }


                return RequestResult<bool>.Success(true);
            }
            catch (FirebaseMessagingException ex)
            {
                return RequestResult<bool>.Failure(ErrorCode.NotFound,$"Firebase error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return RequestResult<bool>.Failure(ErrorCode.NotFound,$"An error occurred: {ex.Message}");
            }
        }
    }
}
