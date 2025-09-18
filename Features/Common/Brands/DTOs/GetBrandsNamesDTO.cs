using AutoMapper;
using KOG.ECommerce.Models.Brands;

namespace KOG.ECommerce.Features.Common.Brands.DTOs
{
    public record GetBrandsNamesDTO( string? ID ,String? Name,int? NumberOfProducts);
    public class GetBrandsNamesDTOProfile : Profile
    {
        public GetBrandsNamesDTOProfile()
        {
            CreateMap<Brand, GetBrandsNamesDTO>();
        }
    }
}
