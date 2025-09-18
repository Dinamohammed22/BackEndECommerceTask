using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Features.WishlistProducts.AddProductToWishlist.Commands;
using KOG.ECommerce.Features.WishlistProducts.AddProductToWishlist.Orchestrator;

namespace KOG.ECommerce.Features.WishlistProducts.AddProductToWishlist
{
    public record AddProductToWishlistRequestViewModel(string ProductId);
    public class AddProductToWishlistRequestValidator : AbstractValidator<AddProductToWishlistRequestViewModel>
    {
        public AddProductToWishlistRequestValidator()
        {
            RuleFor(request => request.ProductId).NotEmpty()
                .WithMessage("ProductId is required.");
        }
    }

    public class AddProductToWishlistEndPointRequestProfile : Profile
    {
        public AddProductToWishlistEndPointRequestProfile()
        {
            CreateMap<AddProductToWishlistRequestViewModel, AddProductToWishlistOrchestrator>();
            CreateMap<AddProductToWishlistOrchestrator, AddProductToWishlistCommand>();
            //CreateMap<AddProductToWishlistCommand, CheckProductById>();


        }
    }
}
