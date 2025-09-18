using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Clients.Queries;
using KOG.ECommerce.Features.Common.Companies.Queries;
using KOG.ECommerce.Features.Common.Users.DTOs;
using KOG.ECommerce.Features.Common.Users.GenerateOTP.Commands;
using KOG.ECommerce.Features.Common.Users.Queries;
using KOG.ECommerce.Features.Common.Users.SendMessage;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Features.Users.OTPLogin.Orchestrators
{
    public record OTPLoginOrchestrator(string Mobile, string Password) : IRequestBase<string>;
    public class OTPLoginOrchestratorHandler : RequestHandlerBase<User, OTPLoginOrchestrator, string>
    {
        public OTPLoginOrchestratorHandler(RequestHandlerBaseParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<string>> Handle(OTPLoginOrchestrator request, CancellationToken cancellationToken)
        {
            var user = _repository.Get(c => c.Mobile == request.Mobile).FirstOrDefault();
            if ((user == null) || !PasswordHasher.Verify(request.Password, user.Password))
            {
                return RequestResult<string>.Failure(ErrorCode.MobileOrPasswordNotCorrect);
            }

            var isApproved = await _repository.AnyAsync(c => c.VerifyStatus == VerifyStatus.Approve && c.ID == user.ID);
            if (!isApproved)
            {
                return RequestResult<string>.Failure(ErrorCode.NotApproved);
            }

            var isActiveClient = await _mediator.Send(new CheckUserActivationQuery(user.ID));
            var isActiveCompany = await _mediator.Send(new CkeckCompanyActivationQuery(user.ID));

            if (!isActiveClient.Data && !isActiveCompany.Data && user.RoleId != Role.SuperAdmin)
            {
                return RequestResult<string>.Failure(ErrorCode.NotActive);
            }

            //const string messageTemplate = "Your OTP for Login is";
            var otpResult = await _mediator.Send(new GenerateOTPCommand(user.ID, request.Mobile));
            //var smsResult = await _mediator.Send(new SendMessageCommand(otpResult.Data.OTP, request.Mobile, messageTemplate));

            return RequestResult<string>.Success(otpResult.Data.OTPtoken, "OTP Generated successfully.");
        }

    }
}
