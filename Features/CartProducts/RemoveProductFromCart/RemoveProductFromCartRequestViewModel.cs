using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.CartProducts.RemoveProductFromCart.Commands;

namespace KOG.ECommerce.Features.CartProducts.RemoveProductFromCart
{
    public record RemoveProductFromCartRequestViewModel(string ProductId);

    public class RemoveProductFromCartRequestValidator : AbstractValidator<RemoveProductFromCartRequestViewModel>
    {
        public RemoveProductFromCartRequestValidator() { }
    }
    public class RemoveProductFromCartRequestEndPointRequestProfile : Profile
    {
        public RemoveProductFromCartRequestEndPointRequestProfile()
        {
            CreateMap<RemoveProductFromCartRequestViewModel, RemoveProductFromCartCommand>();
        }
    }

}
