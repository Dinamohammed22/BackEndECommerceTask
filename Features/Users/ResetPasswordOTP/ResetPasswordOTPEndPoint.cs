using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Users.OTPLogin.Orchestrators;
using KOG.ECommerce.Features.Users.OTPLogin;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Users.ResetPasswordOTP.Orchestrators;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Users.ResetPasswordOTP
{
    public class ResetPasswordOTPEndPoint : EndpointBase<ResetPasswordOTPRequestViewModel, ResetPasswordOTPResponseViewModel>
    {
        public ResetPasswordOTPEndPoint(EndpointBaseParameters<ResetPasswordOTPRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPost]
        //[TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.ResetPasswordOTP })]
        public async Task<EndPointResponse<ResetPasswordOTPResponseViewModel>> ResetPasswordOTP([FromQuery]ResetPasswordOTPRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<ResetPasswordOTPOrchestrator>());
            if (result.IsSuccess)
            {
                return EndPointResponse<ResetPasswordOTPResponseViewModel>.Success(result.Data.MapOne<ResetPasswordOTPResponseViewModel>(), result.Message);
            }
            return EndPointResponse<ResetPasswordOTPResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
