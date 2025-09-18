using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Discounts.BulkActivateDiscount.Commands;

namespace KOG.ECommerce.Features.Discounts.BulkActivateDiscount
{
    public record BulkActivateDiscountRequestViewModel(List<string> Ids);
    public class BulkActivateDiscountRequestValidator : AbstractValidator<BulkActivateDiscountRequestViewModel>
    {
        public BulkActivateDiscountRequestValidator() { }
    }
    public class BulkActivateDiscountRequestProfile : Profile
    {
        public BulkActivateDiscountRequestProfile() {
            CreateMap<BulkActivateDiscountRequestViewModel, BulkActivateDiscountCommand>();
        }
    }
}
