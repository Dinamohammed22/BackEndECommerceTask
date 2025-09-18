using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Discounts.ActivateDiscount.Commands;
using KOG.ECommerce.Features.DiscountProducts.ActivateDiscountToProduct.Commands;

namespace KOG.ECommerce.Features.Discounts.ActivateDiscount
{
    public record ActivatediscountRequestViewModel(string ID);
    public class ActivatediscountRequestValidator : AbstractValidator<ActivatediscountRequestViewModel>
    {
        public ActivatediscountRequestValidator()
        {
            RuleFor(request => request.ID).NotEmpty().Length(1, 100);
        }
    }
    public class ActivatediscountEndPointRequestProfile : Profile
    {
        public ActivatediscountEndPointRequestProfile()
        {
            CreateMap<ActivatediscountRequestViewModel, ActivatediscountCommand>();
        }
    }
}
