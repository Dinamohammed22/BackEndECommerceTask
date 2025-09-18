using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Common.Users.Queries;
using KOG.ECommerce.Helpers;
using Microsoft.IdentityModel.Tokens;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Middlewares;

namespace KOG.ECommerce.Features.Users.CheckOTPValidation
{
    public class CheckOTPValidationEndpoint : EndpointBase<CheckOTPValidationRequestViewModel, CheckOTPValidationResponseViewModel>
    {
        public CheckOTPValidationEndpoint(EndpointBaseParameters<CheckOTPValidationRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpGet]
      //  [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.CheckOTPValidation })]
        public async Task<EndPointResponse<CheckOTPValidationResponseViewModel>> CheckOTPValidation([FromQuery] CheckOTPValidationRequestViewModel viewModel)
        {
            var userID = await _mediator.Send(viewModel.MapOne<CheckOTPValidationQuery>());

            if (!userID.Data.IsNullOrEmpty())
            {
                return EndPointResponse<CheckOTPValidationResponseViewModel>
                    .Success(userID.Data.MapOne<CheckOTPValidationResponseViewModel>(), "OTP Verified successfully");
            }

            return EndPointResponse<CheckOTPValidationResponseViewModel>.Failure(userID.ErrorCode);

        }
    }
}
