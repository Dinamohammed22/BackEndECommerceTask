using AutoMapper;
using KOG.ECommerce.Models.Cities;

namespace KOG.ECommerce.Features.Common.Cities.DTOs
{
    public class CityProfileDTO
    {
        public string? ID { get; set; }
        public string? CityName { get; set; }
        public string? GovernorateName { get; set; }
        public bool IsActive {  get; set; }
    }
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityProfileDTO>()
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.GovernorateName, opt => opt.MapFrom(src => src.Governorate.Name));
        }
    }
}
