using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Features.Users.Login.Orchestrators;

namespace KOG.ECommerce.Features.Users.Login
{
    public class LoginEndPoint : EndpointBase<LoginRequestViewModel, LoginResponseViewModel>
    {
        public LoginEndPoint(EndpointBaseParameters<LoginRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpPost]
       // [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.Login })]
        public async Task<EndPointResponse<LoginResponseViewModel>> Post(LoginRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<LoginOrchestrator>());

            if (result.IsSuccess)
            {
                return EndPointResponse<LoginResponseViewModel>.Success(result.Data.MapOne<LoginResponseViewModel>(), "User Logined successfully");
            }

            return EndPointResponse<LoginResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
