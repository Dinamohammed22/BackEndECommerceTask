using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Coupons.EditCoupon.Commands;

namespace KOG.ECommerce.Features.Coupons.EditCoupon
{
    public record EditCouponRequestViewModel(string ID, string Name, string Code, DateTime StartDate, DateTime EndDate,
     int Amount, int MaxNumOfUser, bool IsActive);
    public class EditCouponRequestValidator : AbstractValidator<EditCouponRequestViewModel>
    {
        public EditCouponRequestValidator() {
            
            RuleFor(x => x.ID)
                .NotEmpty().WithMessage("ID is required.");
            
            RuleFor(x => x.Name)
             .NotEmpty().WithMessage("Coupon name is required.")
             .MinimumLength(2).WithMessage("Coupon name must be at least 2 characters long.");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Coupon code is required.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required.")
                .GreaterThanOrEqualTo(DateTime.Now.Date).WithMessage("Start date cannot be in the past.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("End date is required.")
                .GreaterThan(x => x.StartDate).WithMessage("End date must be after the start date.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");

            RuleFor(x => x.MaxNumOfUser)
                .GreaterThan(0).WithMessage("Max number of users must be greater than zero.");

        }
    }
    public class EditCouponEndPointRequestProfile : Profile
    {
        public EditCouponEndPointRequestProfile()
        {
            CreateMap<EditCouponRequestViewModel, EditCouponCommand>();
        }
    }
}
