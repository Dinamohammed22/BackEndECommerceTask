using AutoMapper;
using KOG.ECommerce.Features.Common.Governorates.DTOs;

namespace KOG.ECommerce.Features.Governorates.GetListGovernorate
{
    public record GetListGovernorateResponseViewModel(string Name);

    public class GetListGovernorateResponseProfile : Profile
    {
        public GetListGovernorateResponseProfile()
        {
            CreateMap<GovernorateProfileDTO, GetListGovernorateResponseViewModel>();
        }
    }
}
