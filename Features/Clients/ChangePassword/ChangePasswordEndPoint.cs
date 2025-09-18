using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Clients.ChangePassword.Orchestrators;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Clients.ChangePassword
{
    public class ChangePasswordEndPoint : EndpointBase<ChangePasswordRequestViewModel, ChangePasswordResponseViewModel>
    {
        public ChangePasswordEndPoint(EndpointBaseParameters<ChangePasswordRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.ChangePassword })]
        public async Task<EndPointResponse<ChangePasswordResponseViewModel>> ChangePassword(ChangePasswordRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<ChangePasswordOrchestrator>());
            if (result.IsSuccess)
                return EndPointResponse<ChangePasswordResponseViewModel>.Success(new ChangePasswordResponseViewModel(), "PassWord changed successfully.");
            else
                return EndPointResponse<ChangePasswordResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
