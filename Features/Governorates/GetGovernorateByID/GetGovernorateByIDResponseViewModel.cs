using AutoMapper;
using KOG.ECommerce.Features.Common.Governorates.DTOs;
using KOG.ECommerce.Features.Governorates.GetGovernorateByName;

namespace KOG.ECommerce.Features.Governorates.GetGovernorateByID
{
    public record GetGovernorateByIDResponseViewModel(string Name,string? GovernorateCode, bool IsActive);

    public class GetGovernorateByIDResponseProfile : Profile
    {
        public GetGovernorateByIDResponseProfile()
        {
            CreateMap<GetGovernorateByIDProfileDTO, GetGovernorateByIDResponseViewModel>();
        }
    }

}
