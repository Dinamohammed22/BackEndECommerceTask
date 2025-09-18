using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Clients.ClientRegister.Orchestrators;
using KOG.ECommerce.Features.Clients.ClientRegister;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Users.OTPLogin.Orchestrators;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Users.OTPLogin
{
    public class OTPLoginEndPoint : EndpointBase<OTPLoginRequestViewModel, OTPLoginResponseViewModel>
    {
        public OTPLoginEndPoint(EndpointBaseParameters<OTPLoginRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPost]
        //[TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.OTPLogin })]
        public async Task<EndPointResponse<OTPLoginResponseViewModel>> Post(OTPLoginRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<OTPLoginOrchestrator>());
            if (result.IsSuccess)
            {
                return EndPointResponse<OTPLoginResponseViewModel>.Success(result.Data.MapOne<OTPLoginResponseViewModel>(), result.Message);
            }
            return EndPointResponse<OTPLoginResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
