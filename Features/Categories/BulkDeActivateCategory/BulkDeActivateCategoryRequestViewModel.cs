using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Categories.BulkDeActivateCategory.Orchisterator;
using KOG.ECommerce.Features.Categories.BulkDeActivateCategory;

namespace KOG.ECommerce.Features.Categories.BulkDeActivateCategory
{
    public record BulkDeActivateCategoryRequestViewModel(List<string> Ids);
    public class BulkDeActivateCategoryRequestValidator : AbstractValidator<BulkDeActivateCategoryRequestViewModel>
    {
        public BulkDeActivateCategoryRequestValidator()
        {
        }
    }
    public class BulkDeActivateCategoryRequestProfile : Profile
    {
        public BulkDeActivateCategoryRequestProfile()
        {
            CreateMap<BulkDeActivateCategoryRequestViewModel, BulkDeActivateCategoryOrchisterator>();
        }
    }
}
