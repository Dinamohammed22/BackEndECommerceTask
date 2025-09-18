using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Discounts.BulkDeactivateDiscount.Commands;

namespace KOG.ECommerce.Features.Discounts.BulkDeactivateDiscount
{
    public record BulkDeactivateDiscountRequestViewModel(List<string> Ids);
    public class BulkDeactivateDiscountRequestValidator : AbstractValidator<BulkDeactivateDiscountRequestViewModel>
    {
        public BulkDeactivateDiscountRequestValidator() { }
    }
    public class BulkDeactivateDiscountRequestProfile : Profile
    {
        public BulkDeactivateDiscountRequestProfile() {
            CreateMap<BulkDeactivateDiscountRequestViewModel, BulkDeactivateDiscountCommand>();
        }
    }
}
