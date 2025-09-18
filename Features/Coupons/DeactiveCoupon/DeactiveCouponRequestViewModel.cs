using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Coupons.ActiveCoupon.Commands;
using KOG.ECommerce.Features.Coupons.ActiveCoupon;
using KOG.ECommerce.Features.Coupons.DeactiveCoupon.Commands;

namespace KOG.ECommerce.Features.Coupons.DeactiveCoupon
{
    public record DeactiveCouponRequestViewModel(string ID);
    public class DeactiveCouponRequestValidator : AbstractValidator<DeactiveCouponRequestViewModel>
    {
        public DeactiveCouponRequestValidator()
        {
            RuleFor(request => request.ID).NotEmpty().Length(1, 100);
        }
    }
    public class DeactiveCouponEndPointRequestProfile : Profile
    {
        public DeactiveCouponEndPointRequestProfile()
        {
            CreateMap<DeactiveCouponRequestViewModel, DeactiveCouponCommand>();
        }
    }
}
