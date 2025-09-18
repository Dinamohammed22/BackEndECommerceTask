using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.CartProducts.AddProductToCart.Commands;
using KOG.ECommerce.Features.Catrs.AddCart.Orchestrators;
using KOG.ECommerce.Features.Catrs.AddProductToCart.Commands;

namespace KOG.ECommerce.Features.Catrs.AddProductToCart
{
    public record AddProductToCartRequestViewModel(string ProductId, int Quantity);
    public class AddProductToCartRequestValidator : AbstractValidator<AddProductToCartRequestViewModel>
    {
        public AddProductToCartRequestValidator()
        {
            RuleFor(request => request.ProductId)
              .NotEmpty().WithMessage("ProductId is required.");

            RuleFor(request => request.Quantity)
                .NotEmpty().WithMessage("Quantity is required.")
                .GreaterThanOrEqualTo(1).WithMessage("Quantity must be greater than or equal To 1.");
        }
    }
    public class AddProductToCartRequestProfile : Profile
    {
        public AddProductToCartRequestProfile()
        {
            CreateMap<AddProductToCartRequestViewModel, AddProductToCartOrchestrator>();
            CreateMap<AddProductToCartOrchestrator, AddCartCommand>();
            CreateMap<AddProductToCartOrchestrator, AddProductToCartCommand>();
        }
    }

}
