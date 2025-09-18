using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.RoleModules.AssignModuleToRole.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.RoleModules.AssignModuleToRole
{
    public class AssignModuleToRoleEndPoint : EndpointBase<AssignModuleToRoleRequestViewModel, AssignModuleToRoleResponseViewModel>
    {
        public AssignModuleToRoleEndPoint(EndpointBaseParameters<AssignModuleToRoleRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPost]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.AssignModulesToRole })]
        public async Task<EndPointResponse<AssignModuleToRoleResponseViewModel>> AssignModulesToRole(AssignModuleToRoleRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<AssignModuleToRoleCommand>());
            if (result.IsSuccess)
                return EndPointResponse<AssignModuleToRoleResponseViewModel>.Success(new AssignModuleToRoleResponseViewModel(), "Modules Assigned To Role successfully");
            else
                return EndPointResponse<AssignModuleToRoleResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
