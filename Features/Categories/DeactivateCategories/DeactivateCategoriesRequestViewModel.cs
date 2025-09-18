using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Categories.ActivateCategories.Commands;
using KOG.ECommerce.Features.Categories.ActivateCategories;
using KOG.ECommerce.Features.Categories.DeactivateCategories.Commands;

namespace KOG.ECommerce.Features.Categories.DeactivateCategories
{
    public record DeactivateCategoriesRequestViewModel(string Id);
    public class DeactivateCategoriesRequestValidator : AbstractValidator<DeactivateCategoriesRequestViewModel>
    {
        public DeactivateCategoriesRequestValidator()
        {
            RuleFor(request => request.Id).NotEmpty().Length(1, 100);

        }
    }
    public class DeactivateCategoriesEndPointRequestProfile : Profile
    {
        public DeactivateCategoriesEndPointRequestProfile()
        {
            CreateMap<DeactivateCategoriesRequestViewModel, DeactivateCategoriesCommand>();
        }
    }
}
