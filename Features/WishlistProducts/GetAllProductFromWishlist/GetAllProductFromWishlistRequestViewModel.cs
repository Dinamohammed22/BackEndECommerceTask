using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.CartProducts.GetAllProductAtCart;
using KOG.ECommerce.Features.Common.CartProducts.Queries;
using KOG.ECommerce.Features.Common.WishlistProducts.Queries;

namespace KOG.ECommerce.Features.WishlistProducts.GetAllProductFromWishlist
{
    public record GetAllProductFromWishlistRequestViewModel();

    public class GetAllProductFromWishlistRequestValidator : AbstractValidator<GetAllProductFromWishlistRequestViewModel>
    {
        public GetAllProductFromWishlistRequestValidator() { }
    }
    public class GetAllProductFromWishlistRequestProfile : Profile
    {
        public GetAllProductFromWishlistRequestProfile()
        {

            CreateMap<GetAllProductFromWishlistRequestViewModel, GetAllProductFromWishlistQuery>();
        }
    }
}
