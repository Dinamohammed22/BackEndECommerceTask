using AutoMapper;
using KOG.ECommerce.Models.Cities;

namespace KOG.ECommerce.Features.Common.Cities.DTOs
{
    public class CitiesForGovernorateProfileDTO
    { 
        public string? Name { get; set; }
    } 

    public class CitiesForGovernorateProfile : Profile
    {
        public CitiesForGovernorateProfile()
        {
            CreateMap<City, CitiesForGovernorateProfileDTO>();
                
        }
    }

}
