using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Companies;

namespace KOG.ECommerce.Features.Companies.CompanyRegister.Commands
{
    public record CompanyRegisterCommand(string ID, string Name, string Mobile, string Address, string GovernorateId,
       string CityId, string? Email, bool IsActive):IRequestBase<bool>;
    public class CompanyRegisterCommandHandler : RequestHandlerBase<Company, CompanyRegisterCommand, bool>
    {
        public CompanyRegisterCommandHandler(RequestHandlerBaseParameters<Company> requestParameters) : base(requestParameters)
        {
        }
        public async override Task<RequestResult<bool>> Handle(CompanyRegisterCommand request, CancellationToken cancellationToken)
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
                    Email = request.Email,
                    IsActive = request.IsActive,
                    CompanyCode = GenerateUniqueNumber()
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
}
