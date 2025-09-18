using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Coupons.DeactiveCoupon.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Coupons.DeactiveCoupon
{
    public class DeactiveCouponEndPoint : EndpointBase<DeactiveCouponRequestViewModel, DeactiveCouponResponseViewModel>
    {
        public DeactiveCouponEndPoint(EndpointBaseParameters<DeactiveCouponRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.DeactiveCoupon })]
        public async Task<EndPointResponse<DeactiveCouponResponseViewModel>> DeactiveCoupon(DeactiveCouponRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<DeactiveCouponCommand>());
            if (result.IsSuccess)
                return EndPointResponse<DeactiveCouponResponseViewModel>.Success(new DeactiveCouponResponseViewModel(), "Coupon Deactivated successfully");
            else
                return EndPointResponse<DeactiveCouponResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
