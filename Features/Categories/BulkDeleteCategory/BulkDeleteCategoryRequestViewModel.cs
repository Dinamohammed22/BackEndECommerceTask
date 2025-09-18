using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Categories.DeleteCategory.Command;
using KOG.ECommerce.Features.Categories.DeleteCategory;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Features.Categories.BulkDeleteCategory.Orchisterator;

namespace KOG.ECommerce.Features.Categories.BulkDeleteCategory
{
    public record BulkDeleteCategoryRequestViewModel(List<string> Ids);
    public class BulkDeleteCategoryRequestValidator : AbstractValidator<BulkDeleteCategoryRequestViewModel>
    {
        public BulkDeleteCategoryRequestValidator()
        {
        }
    }
    public class BulkDeleteCategoryRequestProfile : Profile
    {
        public BulkDeleteCategoryRequestProfile()
        {
            CreateMap<BulkDeleteCategoryRequestViewModel, BulkDeleteCategoryOrchisterator>();
        }
    }
}
