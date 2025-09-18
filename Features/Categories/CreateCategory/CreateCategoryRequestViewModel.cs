using AutoMapper;
using FluentValidation;

using KOG.ECommerce.Features.Categories.CreateCategory.Commands;
using KOG.ECommerce.Features.Categories.CreateCategory.Orchestrators;

namespace KOG.ECommerce.Features.Categories.CreateCategory;
public record CreateCategoryRequestViewModel(string Name, string Description, string? ParentCategoryId, List<string> Tags, List<string> SEO, List<string> Paths, bool IsActive);
public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequestViewModel>
{
    public CreateCategoryRequestValidator()
    {
    }
}
public class CreateCategoryEndPointRequestProfile : Profile
{
    public CreateCategoryEndPointRequestProfile()
    {
        CreateMap<CreateCategoryOrchestrator, CreateCategoryCommand>();
        CreateMap<CreateCategoryRequestViewModel, CreateCategoryOrchestrator>();
    }
}

