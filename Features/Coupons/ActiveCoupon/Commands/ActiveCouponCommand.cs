using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Coupons;

namespace KOG.ECommerce.Features.Coupons.ActiveCoupon.Commands
{
    public record ActiveCouponCommand(string ID) : IRequestBase<bool>;
    public class ActiveCouponCommandHandler : RequestHandlerBase<Coupon, ActiveCouponCommand, bool>
    {
        public ActiveCouponCommandHandler(RequestHandlerBaseParameters<Coupon> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(ActiveCouponCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(b => b.ID == request.ID);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            Coupon coupon = new Coupon { ID = request.ID };
            coupon.IsActive = true;
            _repository.SaveIncluded(coupon, nameof(coupon.IsActive));
            _repository.SaveChanges();
            var result = RequestResult<bool>.Success(true);
            return await Task.FromResult(result);
        }
    }
}
