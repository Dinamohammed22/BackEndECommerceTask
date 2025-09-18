using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Products.DeactivateProducts.Commands;

namespace KOG.ECommerce.Features.Products.DeactivateProducts
{
    public record DeactivateProductsRequestViewModel(string Id);
    public class DeactiveProductsRequestValidator : AbstractValidator<DeactivateProductsRequestViewModel>
    {
        public DeactiveProductsRequestValidator()
        {
            RuleFor(request => request.Id).NotEmpty().Length(1, 100);
        }
    }
    public class DeactivateProductsEndPointRequestProfile : Profile
    {
        public DeactivateProductsEndPointRequestProfile()
        {
            CreateMap<DeactivateProductsRequestViewModel, DeactivateProductsCommand>();
        }
    }
}
