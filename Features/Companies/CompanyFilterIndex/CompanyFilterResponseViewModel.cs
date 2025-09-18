using AutoMapper;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Companies.DTOs;

namespace KOG.ECommerce.Features.Companies.CompanyFilterIndex
{
    public record CompanyFilterResponseViewModel(string ID,
    string? Name, string? Mobile, string? Address, string? GovernorateId, string? GovernorateName,
    string? CityId, string? CityName, string? Activity, string? TaxCardID, string? TaxRegistryNumber,
    string? NID, string? ManagerName, string? ManagerMobile, string? ClassificationId, string? ClassificationName, string? Email);


    public class CompanyFilterResponseProfile : Profile
    {
        public CompanyFilterResponseProfile()
        {
            CreateMap<FilterCompanyProfileDTO, CompanyFilterResponseViewModel>();
        }
    }
}
