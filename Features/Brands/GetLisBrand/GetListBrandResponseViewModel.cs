using AutoMapper;
using KOG.ECommerce.Features.Common.Brands.DTOs;
using KOG.ECommerce.Features.Common.Governorates.DTOs;
using KOG.ECommerce.Features.Governorates.GetListGovernorate;

namespace KOG.ECommerce.Features.Brands.GetLisBrand
{
    public record GetListBrandResponseViewModel(string ID, string Name, bool IsActive, List<string>? Tags, string? Path);

    public class GetListBrandResponseProfile : Profile
    {
        public GetListBrandResponseProfile()
        {
            CreateMap<BrandProfileDTO, GetListBrandResponseViewModel>();
        }
    }

}
