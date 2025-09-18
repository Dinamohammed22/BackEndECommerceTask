using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Discounts.DeactivateDiscountToProduct.Commands;

namespace KOG.ECommerce.Features.Discounts.DeactivateDiscountToProduct
{
    public record DeactivatediscountRequestViewModel(string ID);
    public class DeactivatediscountRequestValidator : AbstractValidator<DeactivatediscountRequestViewModel>
    {
        public DeactivatediscountRequestValidator()
        {
            RuleFor(request => request.ID).NotEmpty().Length(1, 100);
        }
    }
    public class DeactivatediscountEndPointRequestProfile : Profile
    {
        public DeactivatediscountEndPointRequestProfile()
        {
            CreateMap<DeactivatediscountRequestViewModel, DeactivatediscountCommand>();
        }
    }
}
