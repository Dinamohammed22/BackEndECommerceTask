using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.RoleFeatures.AssignAllFeaturesToRole.Commands;
using KOG.ECommerce.Helpers;

namespace KOG.ECommerce.Features.RoleFeatures.AssignAllFeaturesToRole
{
    public class AssignAllFeaturesToRoleEndpoint : EndpointBase<AssignAllFeaturesToRoleRequestViewModel, AssignAllFeaturesToRoleResponseViewModel>
    {
        public AssignAllFeaturesToRoleEndpoint(EndpointBaseParameters<AssignAllFeaturesToRoleRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPost]
       //[TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.AssignAllFeaturesToRole })]
        public async Task<EndPointResponse<AssignAllFeaturesToRoleResponseViewModel>> AssignAllFeaturesToRole()
        {
            var result = await _mediator.Send(new AssignAllFeaturesToRoleCommand());
            if (result.IsSuccess)
                return EndPointResponse<AssignAllFeaturesToRoleResponseViewModel>.Success(new AssignAllFeaturesToRoleResponseViewModel(), "All Features Assigned To Role successfully");
            else
                return EndPointResponse<AssignAllFeaturesToRoleResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
