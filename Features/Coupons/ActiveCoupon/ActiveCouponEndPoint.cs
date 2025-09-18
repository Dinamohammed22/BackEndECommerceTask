using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Coupons.ActiveCoupon.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Coupons.ActiveCoupon
{
    public class ActiveCouponEndPoint : EndpointBase<ActiveCouponRequestViewModel, ActiveCouponResponseViewModel>
    {
        public ActiveCouponEndPoint(EndpointBaseParameters<ActiveCouponRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.ActiveCoupon })]
        public async Task<EndPointResponse<ActiveCouponResponseViewModel>> ActiveCoupon(ActiveCouponRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<ActiveCouponCommand>());
            if (result.IsSuccess)
                return EndPointResponse<ActiveCouponResponseViewModel>.Success(new ActiveCouponResponseViewModel(), "Coupon Activated successfully");
            else
                return EndPointResponse<ActiveCouponResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
