using AutoMapper;
using KOG.ECommerce.Features.Common.WishlistProducts.DTOs;

namespace KOG.ECommerce.Features.WishlistProducts.GetAllProductFromWishlist
{
    public record GetAllProductFromWishlistResponseViewModel(string ID, string Name, double Price, string? Path, int MaximumQuantity, int MinimumQuantity, string CompanyName, string CompanyId);
    public class GetAllProductFromWishlistResponseProfile : Profile
    {
        public GetAllProductFromWishlistResponseProfile()
        {
            CreateMap<GetAllProductFromWishlistDTO, GetAllProductFromWishlistResponseViewModel>();
        }
    }

}
