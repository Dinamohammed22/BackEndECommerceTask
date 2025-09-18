using AutoMapper;
using KOG.ECommerce.Models.CompanyGovernorates;

namespace KOG.ECommerce.Features.Common.CompanyGovernorates.DTOs
{
    public record CompanyGovernorateDTO(string ID,string Name);
    public class CompanyGovernorateDTOProfile : Profile
    {
        public CompanyGovernorateDTOProfile()
        {
            CreateMap<CompanyGovernorate, CompanyGovernorateDTO>();
        }
    }
}
