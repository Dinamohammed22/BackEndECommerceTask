using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Users.ApproveUser.Orchestrators;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Users.ApproveUser
{
    public class ApproveUserEndpoint : EndpointBase<ApproveUserRequestViewModel, ApproveUserResponseViewModel>
    {
        public ApproveUserEndpoint(EndpointBaseParameters<ApproveUserRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.ApproveUser })]
        public async Task<EndPointResponse<ApproveUserResponseViewModel>> Put(ApproveUserRequestViewModel viewModel)
        {
            var result = await _mediator.Send(viewModel.MapOne<ApproveUserOrchestrator>());
            if (result.IsSuccess) {
                return EndPointResponse<ApproveUserResponseViewModel>.Success(new ApproveUserResponseViewModel(), "User Approved successfully");
            }
            return EndPointResponse<ApproveUserResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
