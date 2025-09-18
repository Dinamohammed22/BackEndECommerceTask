using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Users.CreateUser.Commands;
using KOG.ECommerce.Features.Common.Users.GenerateOTP.Commands;
using KOG.ECommerce.Features.Companies.CompanyRegister.Commands;
using KOG.ECommerce.Features.CompanyGovernorates.AddCompanyGovernorates.Commands;
using KOG.ECommerce.Models.Companies;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Companies.CompanyRegister.Orchestrators
{
    public record CompanyRegisterOrchestrator(
    string Name, string Mobile, string Address, string GovernorateId,
    string CityId, string? Email, bool IsActive,  string Password, 
    string ConfirmPassword, List<string> GovernorateIds)
    : IRequestBase<string>;
    public class CompanyRegisterOrchestratorHandler : RequestHandlerBase<Company, CompanyRegisterOrchestrator, string>
    {
        public CompanyRegisterOrchestratorHandler(RequestHandlerBaseParameters<Company> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<string>> Handle(CompanyRegisterOrchestrator request, CancellationToken cancellationToken)
        {
            var userIdResult = await _mediator.Send(new CreateUserCommand(
               Name: request.Name,
               Password: request.Password,
               ConfirmPassword: request.ConfirmPassword,
               Mobile: request.Mobile,
              VerifyStatus: VerifyStatus.Pending,
               RoleId: Role.Company,
               JobTitle: null
           ));

            if (!userIdResult.IsSuccess)
            {
                return await Task.FromResult(RequestResult<string>.Failure(userIdResult.ErrorCode));
            }
            var result = await _mediator.Send(new CompanyRegisterCommand(
                ID: userIdResult.Data,
                Name: request.Name,
                Mobile: request.Mobile,
                Address: request.Address,
                GovernorateId: request.GovernorateId,
                CityId: request.CityId,
                Email: request.Email,
                IsActive: request.IsActive));
            if (!result.IsSuccess)
            {
                return await Task.FromResult(RequestResult<string>.Failure(result.ErrorCode));
            }
            //Message = "Your OTP for registration is";
            var OTPresult = await _mediator.Send(new GenerateOTPCommand(userIdResult.Data, request.Mobile));
            //var SMSResult = await _mediator.Send(new SendMessageCommand(OTPresult.Data.OTP, request.Mobile, Message));
            if (request.GovernorateIds != null || request.GovernorateIds.Any())
            {
                var companyGovernorate = await _mediator.Send(new AddCompanyGovernorateCommand(userIdResult.Data, request.GovernorateIds));
            }
            return await Task.FromResult(RequestResult<string>.Success(OTPresult.Data.OTPtoken));
        }
    }
}
