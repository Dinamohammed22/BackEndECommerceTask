using AutoMapper;
using KOG.ECommerce.Models.Cities;
using KOG.ECommerce.Models.Governorates;

namespace KOG.ECommerce.Features.Common.Cities.DTOs
{
    public record GetCityByIDProfileDTO(string Name,string GovernorateId, bool IsActive);

    public class GetCityByIDProfile : Profile
    {
        public GetCityByIDProfile()
        {
            CreateMap<City, GetCityByIDProfileDTO>();
        }
    }

}
