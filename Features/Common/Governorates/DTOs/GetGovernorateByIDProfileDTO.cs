using AutoMapper;
using KOG.ECommerce.Models.Governorates;

namespace KOG.ECommerce.Features.Common.Governorates.DTOs
{
    public record GetGovernorateByIDProfileDTO(string Name, string? GovernorateCode, bool IsActive);

    public class GetGovernorateByIDProfile : Profile
    {
        public GetGovernorateByIDProfile()
        {
            CreateMap<Governorate, GetGovernorateByIDProfileDTO>();
        }
    }

}
