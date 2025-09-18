using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Coupons.ActiveCoupon.Commands;

namespace KOG.ECommerce.Features.Coupons.ActiveCoupon
{
    public record ActiveCouponRequestViewModel(string ID);
    public class ActiveCouponRequestValidator : AbstractValidator<ActiveCouponRequestViewModel>
    {
        public ActiveCouponRequestValidator()
        {
            RuleFor(request => request.ID).NotEmpty().Length(1, 100);
        }
    }
    public class ActiveCouponEndPointRequestProfile : Profile
    {
        public ActiveCouponEndPointRequestProfile()
        {
            CreateMap<ActiveCouponRequestViewModel, ActiveCouponCommand>();
        }
    }
}
