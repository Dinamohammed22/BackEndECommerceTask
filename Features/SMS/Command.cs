using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Features.SMS
{
    public record Command(string Mobile, string Message):IRequestBase<bool>;
    public class CommandHandler : RequestHandlerBase<User, Command, bool>
    {
        public CommandHandler(RequestHandlerBaseParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var smsResponse = await SMSHelper.SendSmsAsync(request.Mobile, request.Message );

            if (!smsResponse.Success)
                return RequestResult<bool>.Failure(ErrorCode.CannotSend);
            return RequestResult<bool>.Success(true);
        }
    }
}
