using AutoMapper;
using KOG.ECommerce.Features.Common.Companies.DTOs;
using KOG.ECommerce.Models.Classifications;

namespace KOG.ECommerce.Features.Common.Classifications.DTOs
{
    public class GetAllClassificationsDTO
    {
        public string? ID { get; set; }
        public string? Name { get; set; }
        public List<CompanyForClassificationDTO>? Companies { get; set; } = new List<CompanyForClassificationDTO>();
    }
    public class GetAllClassificationsDTOProfile:Profile
    {
        public GetAllClassificationsDTOProfile()
        {
            CreateMap<Classification, GetAllClassificationsDTO>();
        }
    }
}
