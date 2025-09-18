using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Users.GenerateOTP.Commands;
using KOG.ECommerce.Features.Common.Users.Queries;
using KOG.ECommerce.Features.Common.Users.SendMessage;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace KOG.ECommerce.Features.Users.ResendOTP.Orchestrators
{
    public record ResendOTPOrchestrator( string Token) : IRequestBase<string>;
    public class ResendOTPOrchestratorHandler : RequestHandlerBase<User, ResendOTPOrchestrator, string>
    {
        public ResendOTPOrchestratorHandler(RequestHandlerBaseParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<string>> Handle(ResendOTPOrchestrator request, CancellationToken cancellationToken)
        {

            var userInfo = await _repository
                  .Get(u => u.OTPtoken == request.Token)
                  .Select(u => new { u.ID, u.Mobile })
                  .FirstOrDefaultAsync();
            if (userInfo != null)
            {
                var OTPresult = await _mediator.Send(new GenerateOTPCommand(userInfo.ID, userInfo.Mobile));
                string Message = "Your New OTP is";
                //var SMSResult = await _mediator.Send(new SendMessageCommand(OTPresult.Data.OTP, userInfo.Mobile, Message));
                return RequestResult<string>.Success(OTPresult.Data.OTPtoken, "Resend OTP successfully.");
            }

            return RequestResult<string>.Failure(ErrorCode.NotFound);
        }
    }
}
