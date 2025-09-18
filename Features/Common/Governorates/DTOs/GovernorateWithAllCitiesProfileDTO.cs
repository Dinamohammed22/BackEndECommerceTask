using AutoMapper;
using KOG.ECommerce.Features.Common.Cities.DTOs;
using KOG.ECommerce.Models.Governorates;

namespace KOG.ECommerce.Features.Common.Governorates.DTOs
{
    public class GovernorateWithAllCitiesProfileDTO
    {
        public string? ID { get; set; }
        public string? Name { get; set; }
        public string? GovernorateCode {  get; set; }
        public bool IsActive { get; set; }
        public List<CitiesForGovernorateProfileDTO>? Cities { get; set; } = new List<CitiesForGovernorateProfileDTO>();
    }

    public class GovernorateWithAllCitiesProfile : Profile
    {
        public GovernorateWithAllCitiesProfile()
        {
            CreateMap<Governorate, GovernorateWithAllCitiesProfileDTO>();
                
        }
    }
}
