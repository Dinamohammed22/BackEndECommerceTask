using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Categories.DeleteCategory.Command;
using KOG.ECommerce.Features.Common.Products.Queries;

namespace KOG.ECommerce.Features.Categories.DeleteCategory;

public record DeleteCategoryRequestViewModel(string ID);
public class DeleteCategoryRequestValidator : AbstractValidator<DeleteCategoryRequestViewModel>
{
    public DeleteCategoryRequestValidator()
    {
        RuleFor(request => request.ID).NotEmpty().Length(1, 100);
    }
}
public class DeleteCategoryEndPointRequestProfile : Profile
{
    public DeleteCategoryEndPointRequestProfile()
    {
        CreateMap<DeleteCategoryRequestViewModel, DeleteCategoryCammand>();
        CreateMap< DeleteCategoryCammand, CheckIfCategoryHasProductsQuery>();
    }
}

