using AutoMapper;
using KOG.ECommerce.Models.Governorates;

namespace KOG.ECommerce.Features.Common.Governorates.DTOs
{
    public record GovernrateDTO(string Name, string? GovernorateCode, bool IsActive);

    public class GovernrateDTOProfile : Profile
    {
        public GovernrateDTOProfile()
        {
            CreateMap<Governorate, GovernrateDTO>();
        }
    }
}
