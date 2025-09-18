using AutoMapper;
using KOG.ECommerce.Models.Companies;

namespace KOG.ECommerce.Features.Common.Companies.DTOs
{
    public record FilterCompanyProfileDTO(string ID,
    string? Name, string? Mobile, string? Address, string? GovernorateId, string? GovernorateName,
    string? CityId, string? CityName, string? Activity, string? TaxCardID, string? TaxRegistryNumber,
    string? NID, string? ManagerName, string? ManagerMobile, string? ClassificationId ,string? ClassificationName, string? Email);

    public class CompanyFilterProfile : Profile
    {
        public CompanyFilterProfile()
        {

            CreateMap<Company, FilterCompanyProfileDTO>();
        }
    }
}
