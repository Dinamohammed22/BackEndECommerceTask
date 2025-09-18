using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Cities.EditCity.Commands;
using KOG.ECommerce.Features.Cities.EditCity;
using KOG.ECommerce.Features.Categories.EditCategory.Commands;
using KOG.ECommerce.Features.Categories.EditCategory.Orchestrators;

namespace KOG.ECommerce.Features.Categories.EditCategory
{
    public record EditCategoryRequestViewModel(
        string Id, 
        string Name, 
        string Description,
        string? ParentCategoryId,
        List<string> Tags,
        List<string> SEO,
        bool IsActive,
        List<string>? Paths
    );
    public class EditCategoryEndPointRequestValidator : AbstractValidator<EditCategoryRequestViewModel>
    {
        public EditCategoryEndPointRequestValidator()
        {
            // RuleFor(request => request.Id)
            //.NotEmpty().WithMessage("ID is required.")
            //.Length(1, 100).WithMessage("ID must be between 1 and 50 characters.");


            RuleFor(request => request.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 200).WithMessage("Name must be between 2 and 200 characters.");
        }
    }
    public class CreateGroupEndPointRequestProfile : Profile
    {
        public CreateGroupEndPointRequestProfile()
        {
            CreateMap<EditCategoryRequestViewModel, EditCategoryOrchestrator>();
            CreateMap< EditCategoryOrchestrator, EditCategoryCommand>();
        }
    }
}