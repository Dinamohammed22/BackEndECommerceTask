using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Clients.VerifyOTP.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Clients.VerifyOTP
{
    public class VerifyOTPEndPoint : EndpointBase<VerifyOTPRequestViewModel, VerifyOTPResponseViewModel>
    {
        public VerifyOTPEndPoint(EndpointBaseParameters<VerifyOTPRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpPost]
        //[TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.VerifyOTP })]
        public async Task<EndPointResponse<VerifyOTPResponseViewModel>> Post(VerifyOTPRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<VerifyOTPCommand>());
            if (result.IsSuccess)
            {
                return EndPointResponse<VerifyOTPResponseViewModel>.Success(new VerifyOTPResponseViewModel(), "OTP Verified Successfully.");
            }
            return EndPointResponse<VerifyOTPResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
