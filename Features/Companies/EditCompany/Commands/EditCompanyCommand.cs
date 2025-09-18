using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Companies;
namespace KOG.ECommerce.Features.Companies.Commands;

public record EditCompanyCommand(string ID,
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
    int MinimumQuantity)
    : IRequestBase<bool>;

public class EditCompanyCommandHandler : RequestHandlerBase<Company, EditCompanyCommand, bool>
{
    public EditCompanyCommandHandler(RequestHandlerBaseParameters<Company> parameters) : base(parameters) { }

    public async override Task<RequestResult<bool>> Handle(EditCompanyCommand request, CancellationToken cancellationToken)
    {
        var check = await _repository.AnyAsync(b => b.ID == request.ID);
        if (!check)
            return RequestResult<bool>.Failure(ErrorCode.NotFound);
        var phoneValid = _repository.Any(c => c.Mobile == request.Mobile && c.ID!= request.ID);
        if (!phoneValid)
        {
            Company company=new Company { ID = request.ID };
            company.Name = request.Name;
            company.Mobile = request.Mobile;
            company.Address = request.Address;
            company.GovernorateId = request.GovernorateId;
            company.CityId = request.CityId;
            company.Activity = request.Activity;
            company.TaxCardID = request.TaxCardID;
            company.TaxRegistryNumber = request.TaxRegistryNumber;
            company.NID = request.NID;
            company.ManagerName = request.ManagerName;
            company.ManagerMobile = request.ManagerMobile;
            company.ClassificationId = request.ClassificationId;
            company.Email = request.Email;
            company.IsActive= request.IsActive;
            company.Latitude = request.Latitude;
            company.Longitude = request.Longitude;
            company.CreditLimit = request.CreditLimit;
            company.MinimumQuantity = request.MinimumQuantity;
            _repository.SaveIncluded(company,
                nameof(company.Name), nameof(company.Mobile), nameof(company.Address),
                nameof(company.GovernorateId), nameof(company.CityId), nameof(company.Activity),
                nameof(company.TaxCardID), nameof(company.TaxRegistryNumber), nameof(company.NID),
                nameof(company.ManagerName), nameof(company.ManagerMobile), nameof(company.ClassificationId), 
                nameof(company.Email),nameof(company.IsActive),nameof(company.Latitude),nameof(company.Longitude),
                nameof(company.CreditLimit), nameof(company.MinimumQuantity));

            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }
        else
        {
            return await Task.FromResult(RequestResult<bool>.Failure(ErrorCode.ExistMobile));
        }
    }
}
