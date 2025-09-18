using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Coupons.CreateCoupon.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Coupons.CreateCoupon
{
    public class CreateCouponEndPoint : EndpointBase<CreateCouponRequestViewModel, CreateCouponResponseViewModel>
    {
        public CreateCouponEndPoint(EndpointBaseParameters<CreateCouponRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPost]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.AddCoupon })]
        public async Task<EndPointResponse<CreateCouponResponseViewModel>> AddCoupon(CreateCouponRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<CreateCouponCommand>());
            if (result.IsSuccess)
                return EndPointResponse<CreateCouponResponseViewModel>.Success(new CreateCouponResponseViewModel(), "Coupon Added successfully");
            else
                return EndPointResponse<CreateCouponResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
