using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Features.Products.RestockProduct.orchestrator;
using KOG.ECommerce.Features.Products.SelectProductList;

namespace KOG.ECommerce.Features.Products.RestockProduct
{
    public record RestockProductRequestViewModel(string ProductID, int Quantity=1);
    public class RestockProductRequestValidator : AbstractValidator<RestockProductRequestViewModel>
    {
        public RestockProductRequestValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero.");
        }
    }

    public class RestockProductRequestProfile : Profile
    {
        public RestockProductRequestProfile()
        {
            CreateMap<RestockProductRequestViewModel, RestockProductOrchestrator>();
        }
    }
}
