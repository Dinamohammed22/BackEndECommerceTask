using AutoMapper;
using KOG.ECommerce.Features.Common.Cities.DTOs;

namespace KOG.ECommerce.Features.Cities.Queries.GetLisCity
{
    public record GetListCityResponseViewModel(string CityName, string GovernorateName);

    public class GetListCityResponseProfile : Profile
    {
        public GetListCityResponseProfile()
        {
            CreateMap<CityProfileDTO, GetListCityResponseViewModel>()
            .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.CityName))
            .ForMember(dest => dest.GovernorateName, opt => opt.MapFrom(src => src.GovernorateName));
        }
    }

}
