using AutoMapper;
using KOG.ECommerce.Models.Companies;

namespace KOG.ECommerce.Features.Common.Companies.DTOs
{
    public class CompanyForClassificationDTO
    {
        public string? Name { get; set; }
    }
    public class CompanyForClassificationDTOProfile : Profile
    {
        public CompanyForClassificationDTOProfile()
        {
            CreateMap<Company, CompanyForClassificationDTO>();
        }
    }
}
