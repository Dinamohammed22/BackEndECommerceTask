using AutoMapper;
using KOG.ECommerce.Features.Common.Cities.DTOs;

namespace KOG.ECommerce.Features.Cities.Queries.GetByCityNameOrGovernName
{
    public record GetByCityNameOrGovernNameResponseViewModel(string? ID,string? CityName, string? GovernorateName, bool IsActive);

    public class GetByCityNameOrGovernNameResponseProfile : Profile
    {
        public GetByCityNameOrGovernNameResponseProfile()
        {
            CreateMap<CityProfileDTO, GetByCityNameOrGovernNameResponseViewModel>();
        }
    }
}
