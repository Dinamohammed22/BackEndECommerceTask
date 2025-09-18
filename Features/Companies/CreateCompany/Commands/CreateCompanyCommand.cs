using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Data;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Companies;
using KOG.ECommerce.Models.Enums;
using MediatR;

namespace KOG.ECommerce.Features.Companies.Commands;

public record CreateCompanyCommand(string ID,
    string Name, string Mobile, string Address, string GovernorateId,
    string CityId, string? Activity, string TaxCardID, string TaxRegistryNumber,
    string NID, string ManagerName, string ManagerMobile, string ClassificationId,
    string? Email, bool IsActive, double Latitude, double Longitude, string CreditLimit, int MinimumQuantity)
    : IRequestBase<bool>;

public class AddCompanyCommandHandler : RequestHandlerBase<Company, CreateCompanyCommand, bool>
{
    public AddCompanyCommandHandler(RequestHandlerBaseParameters<Company> parameters) : base(parameters)
    {
    }

    public async override Task<RequestResult<bool>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var phoneValid = _repository.Any(c => c.Mobile == request.Mobile);
        if (!phoneValid)
        {

            Company company = new Company
            {
                ID = request.ID,
                Name = request.Name,
                Mobile = request.Mobile,
                Address = request.Address,
                GovernorateId = request.GovernorateId,
                CityId = request.CityId,
                Activity = request.Activity,
                TaxCardID = request.TaxCardID,
                TaxRegistryNumber = request.TaxRegistryNumber,
                NID = request.NID,
                ManagerName = request.ManagerName,
                ManagerMobile = request.ManagerMobile,
                ClassificationId = request.ClassificationId,
                Email = request.Email,
                IsActive = request.IsActive,
                Longitude = request.Longitude,
                Latitude = request.Latitude,
                CreditLimit = request.CreditLimit,
                CompanyCode = GenerateUniqueNumber(),
                MinimumQuantity=request.MinimumQuantity,
            };
            _repository.Add(company);
            _repository.SaveChanges();

            return await Task.FromResult(RequestResult<bool>.Success(true));
        }
        else
        {
            return await Task.FromResult(RequestResult<bool>.Failure(ErrorCode.ExistMobile));
        }
    }
    public static string GenerateUniqueNumber()
    {
        Random _random = new Random();
        var timestampPart = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString().Substring(0, 6);
        var randomPart = _random.Next(10000, 999999999).ToString();
        return timestampPart + randomPart;
    }
}
