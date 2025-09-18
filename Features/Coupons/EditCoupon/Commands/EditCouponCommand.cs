using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Coupons;

namespace KOG.ECommerce.Features.Coupons.EditCoupon.Commands
{
    public record EditCouponCommand(string ID,string Name, string Code, DateTime StartDate, DateTime EndDate,
     int Amount, int MaxNumOfUser, bool IsActive) :IRequestBase<bool>;
    public class EditCouponCommandHandler : RequestHandlerBase<Coupon, EditCouponCommand, bool>
    {
        public EditCouponCommandHandler(RequestHandlerBaseParameters<Coupon> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(EditCouponCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(b => b.ID == request.ID);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            Coupon coupon = new Coupon { ID = request.ID };
            coupon.Name=request.Name;
            coupon.Code=request.Code;
            coupon.Amount=request.Amount;
            coupon.MaxNumOfUser=request.MaxNumOfUser;
            coupon.StartDate=request.StartDate;
            coupon.EndDate=request.EndDate;
            coupon.IsActive=request.IsActive;
            _repository.SaveIncluded(coupon,nameof(coupon.Name), nameof(coupon.Code), nameof(coupon.Amount),
                nameof(coupon.MaxNumOfUser), nameof(coupon.StartDate), nameof(coupon.EndDate),nameof(coupon.IsActive));
            _repository.SaveChanges();

            var result = RequestResult<bool>.Success(true);
            return await Task.FromResult(result);
        }
    }
}
