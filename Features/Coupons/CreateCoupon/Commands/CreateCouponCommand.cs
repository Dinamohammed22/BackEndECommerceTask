using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Coupons;

namespace KOG.ECommerce.Features.Coupons.CreateCoupon.Commands
{
    public record CreateCouponCommand(string Name , string Code, DateTime StartDate, DateTime EndDate ,
     int Amount , int MaxNumOfUser , string CompanyId, bool IsActive) :IRequestBase<bool>;
    public class CreateCouponCommandHandler : RequestHandlerBase<Coupon, CreateCouponCommand, bool>
    {
        public CreateCouponCommandHandler(RequestHandlerBaseParameters<Coupon> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
        {
            Coupon coupon = new Coupon { 
                Name = request.Name ,
                Code = request.Code ,
                StartDate = request.StartDate ,
                EndDate = request.EndDate ,
                Amount = request.Amount ,
                MaxNumOfUser = request.MaxNumOfUser ,
                CompanyId = request.CompanyId ,
                IsActive = request.IsActive ,   
            };
            _repository.Add(coupon);
            _repository.SaveChanges();

            var result = RequestResult<bool>.Success(true);

            return await Task.FromResult(result);
        }
    }
}
