using AutoMapper;
using KOG.ECommerce.Features.Common.Cities.DTOs;
using KOG.ECommerce.Features.Common.Governorates.DTOs;
using KOG.ECommerce.Features.Governorates.GetListGovernorate;

namespace KOG.ECommerce.Features.Governorates.GetAllGovernorateWithAllCities
{
    public record GetAllGovernorateWithAllCitiesResponseViewModel(string? ID,string? Name, string? GovernorateCode, bool IsActive, List<CitiesForGovernorateProfileDTO> Cities);
    public class GetAllGovernorateWithAllCitiesProfile : Profile
    {
        public GetAllGovernorateWithAllCitiesProfile()
        {
            CreateMap<GovernorateWithAllCitiesProfileDTO, GetAllGovernorateWithAllCitiesResponseViewModel>();
        }
    }
}
