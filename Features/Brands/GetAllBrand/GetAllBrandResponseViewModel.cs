using AutoMapper;
using KOG.ECommerce.Features.Common.Brands.DTOs;

namespace KOG.ECommerce.Features.Brands.GetAllBrand
{
    public record GetAllBrandResponseViewModel(string ID,string Name, string? Path);
    public class GetAllBrandResponseProfile : Profile
    {
        public GetAllBrandResponseProfile()
        {
            CreateMap<GetAllBrandDTO, GetAllBrandResponseViewModel>();
        }
    }
}
