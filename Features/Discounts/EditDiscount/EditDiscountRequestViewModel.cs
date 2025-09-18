using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Discounts.EditDiscount.Commands;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Discounts.EditDiscount
{
    public record EditDiscountRequestViewModel(string ID, string Name, DiscountCategory DiscountCategory, DiscountType DiscountType,
        double? ReceiptAmount, int? Quantity, double Amount, DateTime StartDate, DateTime EndDate, bool IsActive);

    public class EditDiscountRequestValidator : AbstractValidator<EditDiscountRequestViewModel>
    {
        public EditDiscountRequestValidator()
        {
            RuleFor(x => x.ID)
                .NotEmpty().WithMessage("ID is required.");

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

    public class EditDiscountRequestProfile : Profile
    {
        public EditDiscountRequestProfile()
        {
            CreateMap<EditDiscountRequestViewModel, EditDiscountCommand>();
        }
    }
}
