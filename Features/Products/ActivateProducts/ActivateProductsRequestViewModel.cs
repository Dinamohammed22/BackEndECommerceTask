using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Products.ActivateProducts.Commands;

namespace KOG.ECommerce.Features.Products.ActivateProducts
{
    public record ActivateProductsRequestViewModel(string Id);
    public class ActiveProductsRequestValidator : AbstractValidator<ActivateProductsRequestViewModel>
    {
        public ActiveProductsRequestValidator()
        {
            RuleFor(request => request.Id).NotEmpty().Length(1, 100);
        }
    }
    public class ActivateProductsEndPointRequestProfile : Profile
    {
        public ActivateProductsEndPointRequestProfile()
        {
            CreateMap<ActivateProductsRequestViewModel, ActivateProductsCommand>();
        }
    }

}
