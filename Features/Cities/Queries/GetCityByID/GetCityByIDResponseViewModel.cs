using AutoMapper;
using KOG.ECommerce.Features.Common.Cities.DTOs;

namespace KOG.ECommerce.Features.Cities.Queries.GetCityByID
{
    public record GetCityByIDResponseViewModel(string Name,string GovernorateId, bool IsActive);

    public class GetCityByIDResponseProfile : Profile
    {
        public GetCityByIDResponseProfile()
        {
            CreateMap<GetCityByIDProfileDTO, GetCityByIDResponseViewModel>();
        }
    }

}
