using AutoMapper;
using KOG.ECommerce.Features.Common.Cities.DTOs;

namespace KOG.ECommerce.Features.Cities.Queries.GetCitiesByGovernorateID
{
    public record GetCitiesByGovernorateIDResponseViewModel(string Name);

    public class GetCitiesByGovernorateIDResponseProfile : Profile
    {
        public GetCitiesByGovernorateIDResponseProfile()
        {
            CreateMap<GetCitiesByGovernorateIDProfileDTO, GetCitiesByGovernorateIDResponseViewModel>();
        }
    }

}
