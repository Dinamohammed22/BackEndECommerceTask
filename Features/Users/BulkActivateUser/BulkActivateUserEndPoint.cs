using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Users.ActivateUser.Commands;
using KOG.ECommerce.Features.Users.ActivateUser;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Users.BulkActivateUser.Orchestrator;
using KOG.ECommerce.Helpers;

namespace KOG.ECommerce.Features.Users.BulkActivateUser
{
    public class BulkActivateUserEndPoint : EndpointBase<BulkActivateUserRequestViewModel, BulkActivateUserResponseViewModel>
    {
        public BulkActivateUserEndPoint(EndpointBaseParameters<BulkActivateUserRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.BulkActivateUser })]
        public async Task<EndPointResponse<BulkActivateUserResponseViewModel>> BulkActivateUser(BulkActivateUserRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<BulkActivateUserOrchestrator>());
            if (result.IsSuccess)
                return EndPointResponse<BulkActivateUserResponseViewModel>.Success(new BulkActivateUserResponseViewModel(), "Bulk Users Activated successfully.");
            else
                return EndPointResponse<BulkActivateUserResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
