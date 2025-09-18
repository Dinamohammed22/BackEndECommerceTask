using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Coupons.EditCoupon.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Coupons.EditCoupon
{
    public class EditCouponEndPoint : EndpointBase<EditCouponRequestViewModel, EditCouponResponseViewModel>
    {
        public EditCouponEndPoint(EndpointBaseParameters<EditCouponRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.EditCoupon })]
        public async Task<EndPointResponse<EditCouponResponseViewModel>> EditCoupon(EditCouponRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<EditCouponCommand>());
            if (result.IsSuccess)
                return EndPointResponse<EditCouponResponseViewModel>.Success(new EditCouponResponseViewModel(), "Coupon Updated successfully");
            else
                return EndPointResponse<EditCouponResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
