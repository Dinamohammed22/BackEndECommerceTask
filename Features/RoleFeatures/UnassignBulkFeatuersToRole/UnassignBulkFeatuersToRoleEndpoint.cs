using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.RoleFeatures.UnassignBulkFeaturesFromRole.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.RoleFeatures.UnassignBulkFeatuersToRole
{
    public class UnassignBulkFeatuersToRoleEndpoint : EndpointBase<UnassignBulkFeatuersToRoleRequestViewModel, UnassignBulkFeatuersToRoleResponseViewModel>
    {
        public UnassignBulkFeatuersToRoleEndpoint(EndpointBaseParameters<UnassignBulkFeatuersToRoleRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.UnassignBulkFeaturesToRole })]
        public async Task<EndPointResponse<UnassignBulkFeatuersToRoleResponseViewModel>> UnassignBulkFeaturesToRole(UnassignBulkFeatuersToRoleRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<UnassignBulkFeaturesFromRoleCommand>());
            if (result.IsSuccess)
                return EndPointResponse<UnassignBulkFeatuersToRoleResponseViewModel>.Success(new UnassignBulkFeatuersToRoleResponseViewModel(), "Bulk Features Unassigned To Role successfully");
            else
                return EndPointResponse<UnassignBulkFeatuersToRoleResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
