using AutoMapper;
using KOG.ECommerce.Features.Common.Brands.DTOs;
using KOG.ECommerce.Features.Common.Governorates.DTOs;
using KOG.ECommerce.Features.Governorates.GetGovernorateByName;

namespace KOG.ECommerce.Features.Brands.GetBrandByName
{
    public record GetBrandByNameResponseViewModel(string Name);

    public class GetBrandByNameResponseProfile : Profile
    {
        public GetBrandByNameResponseProfile()
        {
            CreateMap<BrandProfileDTO, GetBrandByNameResponseViewModel>();
        }
    }
}
