using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Clients.Queries;
using KOG.ECommerce.Features.Common.Companies.Queries;
using KOG.ECommerce.Features.Common.Users.GenerateOTP.Commands;
using KOG.ECommerce.Features.Common.Users.Queries;
using KOG.ECommerce.Features.Common.Users.SendMessage;
using KOG.ECommerce.Features.Users.OTPLogin.Orchestrators;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Features.Users.ResetPasswordOTP.Orchestrators
{
    public record ResetPasswordOTPOrchestrator(string Mobile) : IRequestBase<string>;
    public class ResetPasswordOTPHandler : RequestHandlerBase<User, ResetPasswordOTPOrchestrator, string>
    {
        public ResetPasswordOTPHandler(RequestHandlerBaseParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<string>> Handle(ResetPasswordOTPOrchestrator request, CancellationToken cancellationToken)
        {
            var user = _repository.Get(c => c.Mobile == request.Mobile).FirstOrDefault();
            if (user != null)
            {
                var IsApproved = await _repository.AnyAsync(c => c.VerifyStatus == VerifyStatus.Approve && c.ID == user.ID);
                if (IsApproved)
                {
                    var IsActiveClient = await _mediator.Send(new CheckUserActivationQuery(user.ID));
                    var IsActiveCompany = await _mediator.Send(new CkeckCompanyActivationQuery(user.ID));
                    if (IsActiveClient.Data || IsActiveCompany.Data)
                    {
                        string Message = "Your OTP for Login is";
                        var OTPresult = await _mediator.Send(new GenerateOTPCommand(user.ID, request.Mobile));
                        //var SMSResult = await _mediator.Send(new SendMessageCommand(OTPresult.Data.OTP, request.Mobile, Message));
                        return RequestResult<string>.Success(OTPresult.Data.OTPtoken, "OTP Generated successfully.");
                    }
                    return RequestResult<string>.Failure(ErrorCode.NotActive);
                }
                return RequestResult<string>.Failure(ErrorCode.NotApproved);
            }
            return RequestResult<string>.Failure(ErrorCode.NoAccountForMobile);
        }
    }
}
