using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Users.EditUser.Commands;
using KOG.ECommerce.Features.Companies.Commands;
using KOG.ECommerce.Features.Medias.SaveMedia.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Governorates;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Features.Companies.EditCompany.Orchestrators
{
    public record EditCompanyOrchestrator(string ID,
    string? Email,
    double Latitude,
    double Longitude,
    string? Activity,
    string TaxCardID,
    string TaxRegistryNumber,
    string CreditLimit,
    string NID,
    string ManagerName,
    string ManagerMobile,
    bool IsActive,
    string Name,
    string Mobile,
    string GovernorateId,
    string CityId,
    string Address,
    string ClassificationId,
    int MinimumQuantity,
    List<string> CompanyFiles,
    List<string> CompanyImage,
    List<string> GovernorateIds
    ) : IRequestBase<bool>;
    public class EditCompanyOrchestratorHandler : RequestHandlerBase<User, EditCompanyOrchestrator, bool>
    {
        public EditCompanyOrchestratorHandler(RequestHandlerBaseParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(EditCompanyOrchestrator request, CancellationToken cancellationToken)
        {
            var UserName = request.Name;
            var userIdResult = await _mediator.Send(new EditUserCommand(request.ID,request.Name,request.Mobile,null));
            if (!userIdResult.IsSuccess)
            {
                return await Task.FromResult(RequestResult<bool>.Failure(userIdResult.ErrorCode));
            }
            var result = await _mediator.Send(request.MapOne<EditCompanyCommand>());
            if (!result.IsSuccess)
            {
                return await Task.FromResult(RequestResult<bool>.Failure(result.ErrorCode));
            }
            var image = await _mediator.Send(new SaveMediaCommand(
               SourceId: request.ID,
               SourceType: SourceType.CompanyImage,
               Paths: request.CompanyImage
               ));
            if (!image.IsSuccess)
            {
                return await Task.FromResult(RequestResult<bool>.Failure(image.ErrorCode));
            }
            var files = await _mediator.Send(new SaveMediaCommand(
             SourceId:request.ID,
             SourceType: SourceType.CompanyFiles,
             Paths: request.CompanyFiles
             ));
            if (!files.IsSuccess)
            {
                return await Task.FromResult(RequestResult<bool>.Failure(image.ErrorCode));
            }
            var govResult = await _mediator.Send(new UpdateCompanyGovernoratesCommand(request.ID,request.GovernorateIds));
            if (!govResult.IsSuccess)
                return RequestResult<bool>.Failure(govResult.ErrorCode);

            return await Task.FromResult(RequestResult<bool>.Success(true));
        }
    }
}
