using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Users.CreateUser.Commands;
using KOG.ECommerce.Features.Companies.Commands;
using KOG.ECommerce.Features.CompanyGovernorates.AddCompanyGovernorates.Commands;
using KOG.ECommerce.Features.Medias.SaveMedia.Commands;
using KOG.ECommerce.Models.Companies;
using KOG.ECommerce.Models.Enums;
namespace KOG.ECommerce.Features.Companies.CreateCompany.Orchestrators
{
    public record CreateCompanyOrchestrator(
    string Name, string Mobile, string Address, string GovernorateId,
    string CityId, string? Activity, string TaxCardID, string TaxRegistryNumber,
    string NID, string ManagerName, string ManagerMobile, string ClassificationId,
    string? Email, bool IsActive, double Latitude, double Longitude, string CreditLimit, int MinimumQuantity,
    string Password, string ConfirmPassword, List<string> CompanyImage,List<string> CompanyFiles, List<string> GovernorateIds)
    : IRequestBase<bool>;
    public class CreateCompanyOrchestratorHandler : RequestHandlerBase<Company, CreateCompanyOrchestrator, bool>
    {
        public CreateCompanyOrchestratorHandler(RequestHandlerBaseParameters<Company> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(CreateCompanyOrchestrator request, CancellationToken cancellationToken)
        {
            var userIdResult = await _mediator.Send(new CreateUserCommand(
               Name: request.Name,
               Password: request.Password,
               ConfirmPassword:request.ConfirmPassword,
               Mobile: request.Mobile,
              VerifyStatus:VerifyStatus.Approve,
               RoleId: Role.Company,
               JobTitle:null
           ));

            if (!userIdResult.IsSuccess)
            {
                return await Task.FromResult(RequestResult<bool>.Failure(userIdResult.ErrorCode));
            }
            var result = await _mediator.Send(new CreateCompanyCommand(
                ID: userIdResult.Data,
                Name: request.Name,
                Mobile: request.Mobile,
                Address: request.Address,
                GovernorateId: request.GovernorateId,
                CityId: request.CityId,
                Activity: request.Activity,
                TaxCardID: request.TaxCardID,
                TaxRegistryNumber: request.TaxRegistryNumber,
                NID: request.NID,
                ManagerName: request.ManagerName,
                ManagerMobile: request.ManagerMobile,
                ClassificationId: request.ClassificationId,
                Email: request.Email,
                IsActive: request.IsActive,
                Latitude:request.Latitude,
                Longitude:request.Longitude,
                CreditLimit:request.CreditLimit,
                MinimumQuantity:request.MinimumQuantity));
            if (!result.IsSuccess)
            {
                return await Task.FromResult(RequestResult<bool>.Failure(result.ErrorCode));
            }
            var image = await _mediator.Send(new SaveMediaCommand(
                SourceId: userIdResult.Data,
                SourceType: SourceType.CompanyImage,
                Paths: request.CompanyImage
                ));
            if (!image.IsSuccess)
            {
                return await Task.FromResult(RequestResult<bool>.Failure(image.ErrorCode));
            }
            var files = await _mediator.Send(new SaveMediaCommand(
             SourceId: userIdResult.Data,
             SourceType: SourceType.CompanyFiles,
             Paths: request.CompanyFiles
             ));
            if (!files.IsSuccess)
            {
                return await Task.FromResult(RequestResult<bool>.Failure(image.ErrorCode));
            }
            if ( request.GovernorateIds != null || request.GovernorateIds.Any())
            {
                var companyGovernorate = await _mediator.Send(new AddCompanyGovernorateCommand(userIdResult.Data, request.GovernorateIds));
            }
            return await Task.FromResult(RequestResult<bool>.Success(true));
        }
    }

}
