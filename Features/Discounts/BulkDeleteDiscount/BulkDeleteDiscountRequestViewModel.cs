using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Discounts.BulkDeleteDiscount.Commands;

namespace KOG.ECommerce.Features.Discounts.BulkDeleteDiscount
{
    public record BulkDeleteDiscountRequestViewModel(List<string>Ids);
    public class BulkDeleteDiscountRequestValidator : AbstractValidator<BulkDeleteDiscountRequestViewModel>
    {
        public BulkDeleteDiscountRequestValidator()
        {
        }
    }
    public class BulkDeleteDiscountRequestProfile : Profile
    {
        public BulkDeleteDiscountRequestProfile() {
            CreateMap<BulkDeleteDiscountRequestViewModel, BulkDeleteDiscountCommand>();
        }
    }
}
