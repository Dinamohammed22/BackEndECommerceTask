using AutoMapper;
using KOG.ECommerce.Models.Cities;

namespace KOG.ECommerce.Features.Common.Cities.DTOs
{
    public record CityDTO(string Name,string GovernorateId,bool IsActive);
    public class CityDTOProfile : Profile
    {
        public CityDTOProfile()
        {
            CreateMap<City, CityDTOProfile>();
        }
    }
}
