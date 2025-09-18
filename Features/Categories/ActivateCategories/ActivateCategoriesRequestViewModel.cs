using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Categories.ActivateCategories.Commands;

namespace KOG.ECommerce.Features.Categories.ActivateCategories
{
    public record ActivateCategoriesRequestViewModel(string Id);
    public class ActivateCategoriesRequestValidator : AbstractValidator<ActivateCategoriesRequestViewModel>
    {
        public ActivateCategoriesRequestValidator()
        {
            RuleFor(request => request.Id).NotEmpty().Length(1, 100);

        }
    }
    public class ActiveCategoriesEndPointRequestProfile : Profile
    {
        public ActiveCategoriesEndPointRequestProfile()
        {
            CreateMap<ActivateCategoriesRequestViewModel, ActivateCategoriesCommand>();
        }
    }
}
