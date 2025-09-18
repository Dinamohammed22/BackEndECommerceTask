using AutoMapper;
using KOG.ECommerce.Features.Common.OrderItems.DTOs;
using KOG.ECommerce.Models.OrderItems;
using KOG.ECommerce.Models.WishlistProducts;

namespace KOG.ECommerce.Features.Common.WishlistProducts.DTOs
{
    public record GetAllProductFromWishlistDTO(
        string ID,
        string Name,
        double Price,
        string? Path,
        int MaximumQuantity,
        int MinimumQuantity, string CompanyName, string CompanyId
    );

    public class GetAllProductFromWishlistDTOProfile : Profile
    {
        public GetAllProductFromWishlistDTOProfile()
        {
            CreateMap<WishlistProduct, GetAllProductFromWishlistDTO>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
               .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
               .ForMember(dest => dest.MaximumQuantity, opt => opt.MapFrom(src => src.Product.MaximumQuantity))
               .ForMember(dest => dest.MinimumQuantity, opt => opt.MapFrom(src => src.Product.MinimumQuantity))
               .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Product.Company.Name))
               .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Product.Company.ID.ToString()));
            ;
        }
    }
}
