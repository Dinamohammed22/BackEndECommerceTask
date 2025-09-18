using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Coupons.CreateCoupon.Commands;

namespace KOG.ECommerce.Features.Coupons.CreateCoupon
{
    public record CreateCouponRequestViewModel(string Name, string Code, DateTime StartDate, DateTime EndDate,
     int Amount, int MaxNumOfUser, string CompanyId, bool IsActive);
    public class CreateCouponRequestValidator : AbstractValidator<CreateCouponRequestViewModel>
    {
        public CreateCouponRequestValidator() {
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

            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("Company ID is required.");

        }
    }
    public class CreateCouponEndPointRequestProfile : Profile
    {
        public CreateCouponEndPointRequestProfile()
        {
            CreateMap<CreateCouponRequestViewModel, CreateCouponCommand>();
        }
    }


}
