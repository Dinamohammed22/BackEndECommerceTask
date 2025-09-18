using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.RoleFeatures.AssignBulkFeaturesToRole.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.RoleFeatures.AssignBulkFeatuersToRole
{
    public class AssignBulkFeatuersToRoleEndPoint : EndpointBase<AssignBulkFeatuersToRoleRequestViewModel, AssignBulkFeatuersToRoleResponseViewModel>
    {
        public AssignBulkFeatuersToRoleEndPoint(EndpointBaseParameters<AssignBulkFeatuersToRoleRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpPost]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.AssignBulkFeaturesToRole })]
        public async Task<EndPointResponse<AssignBulkFeatuersToRoleResponseViewModel>> AssignBulkFeaturesToRole(AssignBulkFeatuersToRoleRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<AssignBulkFeatuersToRoleOrchestrator>());
            if (result.IsSuccess)
                return EndPointResponse<AssignBulkFeatuersToRoleResponseViewModel>.Success(new AssignBulkFeatuersToRoleResponseViewModel(), "Bulk Features Assigned To Role successfully");
            else
                return EndPointResponse<AssignBulkFeatuersToRoleResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
