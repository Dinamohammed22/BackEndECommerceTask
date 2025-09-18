using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Cities.DeleteCity.Commands;
using KOG.ECommerce.Features.Cities.DeleteCity;
using KOG.ECommerce.Features.WishlistProducts.DeleteProductFromWishlist.Commands;

namespace KOG.ECommerce.Features.WishlistProducts.DeleteProductFromWishlist
{
    public record DeleteProductFromWishlistRequestViewModel(string productId);

    public class DeleteProductFromWishlistRequestValidator : AbstractValidator<DeleteProductFromWishlistRequestViewModel>
    {
        public DeleteProductFromWishlistRequestValidator()
        {
            RuleFor(request => request.productId)
            .NotEmpty().WithMessage("productId is required.")
            .Length(1, 100).WithMessage("productId must be between 1 and 100 characters.");

        }
    }
    public class DeleteProductFromWishlistRequestProfile : Profile
    {
        public DeleteProductFromWishlistRequestProfile()
        {
            CreateMap<DeleteProductFromWishlistRequestViewModel, DeleteProductFromWishlistCommand>();

        }
    }
}
