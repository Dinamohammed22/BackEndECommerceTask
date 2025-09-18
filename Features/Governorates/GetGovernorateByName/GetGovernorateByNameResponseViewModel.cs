using AutoMapper;
using KOG.ECommerce.Features.Common.Governorates.DTOs;

namespace KOG.ECommerce.Features.Governorates.GetGovernorateByName
{
    public record GetGovernorateByNameResponseViewModel(string Name);

    public class GetGovernorateByNameResponseProfile : Profile
    {
        public GetGovernorateByNameResponseProfile()
        {
            CreateMap<GovernorateProfileDTO, GetGovernorateByNameResponseViewModel>();
        }
    }
}
