using AutoMapper;
using KOG.ECommerce.Models.Cities;

namespace KOG.ECommerce.Features.Common.Cities.DTOs
{
    public record GetCitiesByGovernorateIDProfileDTO(string Name);

    public class GetCitiesByGovernorateIDProfile : Profile
    {
        public GetCitiesByGovernorateIDProfile()
        {
            CreateMap<City, GetCitiesByGovernorateIDProfileDTO>();
        }
    }

}
