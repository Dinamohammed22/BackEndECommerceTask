using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Categories.BulkDeleteCategory.Orchisterator;
using KOG.ECommerce.Features.Categories.BulkDeleteCategory;
using KOG.ECommerce.Features.Categories.BulkActivateCategory.Orchisterator;

namespace KOG.ECommerce.Features.Categories.BulkActivateCategory
{
    public record BulkActivateCategoryRequestViewModel(List<string> Ids);
    public class BulkActivateCategoryRequestValidator : AbstractValidator<BulkActivateCategoryRequestViewModel>
    {
        public BulkActivateCategoryRequestValidator()
        {
        }
    }
    public class BulkActivateCategoryRequestProfile : Profile
    {
        public BulkActivateCategoryRequestProfile()
        {
            CreateMap<BulkActivateCategoryRequestViewModel, BulkActivateCategoryOrchisterator>();
        }
    }
}
