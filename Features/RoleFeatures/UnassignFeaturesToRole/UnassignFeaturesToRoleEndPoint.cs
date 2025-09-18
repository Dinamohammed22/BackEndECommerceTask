using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.RoleFeatures.UnassignFeaturesToRole.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.RoleFeatures.UnassignFeaturesToRole
{
    public class UnassignFeaturesToRoleEndPoint : EndpointBase<UnassignFeaturesToRoleRequestViewModel, UnassignFeaturesToRoleResponseViewModel>
    {
        public UnassignFeaturesToRoleEndPoint(EndpointBaseParameters<UnassignFeaturesToRoleRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpDelete]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.UnassignFeaturesfromRole })]
        public async Task<EndPointResponse<UnassignFeaturesToRoleResponseViewModel>> UnassignFeaturesToRole(UnassignFeaturesToRoleRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<UnassignFeaturesToRoleCommand>());
            if (result.IsSuccess)
                return EndPointResponse<UnassignFeaturesToRoleResponseViewModel>.Success(new UnassignFeaturesToRoleResponseViewModel(), "Features Unassigned To Role successfully");
            else
                return EndPointResponse<UnassignFeaturesToRoleResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
