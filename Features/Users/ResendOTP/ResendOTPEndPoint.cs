using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Clients.ClientRegister.Orchestrators;
using KOG.ECommerce.Features.Clients.ClientRegister;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Users.ResendOTP.Orchestrators;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Users.ResendOTP 
{
    public class ResendOTPEndPoint : EndpointBase<ResendOTPRequestViewModel, ResendOTPResponseViewModel>
    {
        public ResendOTPEndPoint(EndpointBaseParameters<ResendOTPRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPost]
       // [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.ResendOTP })]
        public async Task<EndPointResponse<ResendOTPResponseViewModel>> Post(ResendOTPRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<ResendOTPOrchestrator>());
            if (result.IsSuccess)
            {
                return EndPointResponse<ResendOTPResponseViewModel>.Success(result.Data.MapOne<ResendOTPResponseViewModel>(), result.Message);
            }
            return EndPointResponse<ResendOTPResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
