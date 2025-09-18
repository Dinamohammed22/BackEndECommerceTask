using AutoMapper;
using KOG.ECommerce.Models.Governorates;

namespace KOG.ECommerce.Features.Common.Governorates.DTOs
{
    public record GovernorateProfileDTO(string Name);

    public class GovernorateProfile : Profile
    {
        public GovernorateProfile()
        {
            //CreateMap<Governorate, GovernorateProfileDTO>();
            CreateMap<Governorate, GovernorateProfileDTO>();
        }
    }
}
