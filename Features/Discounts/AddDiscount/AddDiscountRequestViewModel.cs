using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Discounts.Queries;
using KOG.ECommerce.Features.Discounts.AddDiscount.Commands;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Discounts.AddDiscount
{
    public record AddDiscountRequestViewModel(string Name, DiscountCategory DiscountCategory, DiscountType DiscountType, int? Quantity, double? ReceiptAmount, double Amount, DateTime StartDate, DateTime EndDate, bool IsActive);

    public class AddDiscountToProductRequestValidator : AbstractValidator<AddDiscountRequestViewModel>
    {
        public AddDiscountToProductRequestValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.DiscountCategory)
                .IsInEnum().WithMessage("Invalid discount category.");

            RuleFor(x => x.DiscountType)
                .IsInEnum().WithMessage("Invalid discount type.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Discount amount must be greater than 0.");

            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate).WithMessage("Start date must be before the end date.")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Start date cannot be in the past.");

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate).WithMessage("End date must be after the start date.");
        }
    }


    public class AddDiscountToProductEndPointRequestProfile : Profile
    {
        public AddDiscountToProductEndPointRequestProfile()
        {
            CreateMap<AddDiscountRequestViewModel, AddDiscountCommand>();
            CreateMap<AddDiscountCommand, CheckDiscountQuery>();
        
        }
    }
}
