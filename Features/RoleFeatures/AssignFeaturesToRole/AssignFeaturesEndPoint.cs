using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Features.RoleFeatures.AssignFeaturesToRole.Commands;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.RoleFeatures.AssignFeaturesToRole
{
    public class AssignFeaturesEndPoint : EndpointBase<AssignFeaturesRequestViewModel, AssignFeaturesResponseViewModel>
    {
        public AssignFeaturesEndPoint(EndpointBaseParameters<AssignFeaturesRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpPost]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.AssignFeaturesToRole })]
        public async Task<EndPointResponse<AssignFeaturesResponseViewModel>> AssignFeaturesToRole(AssignFeaturesRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<AssignFeaturesToRoleCommand>());
            if (result.IsSuccess)
                return EndPointResponse<AssignFeaturesResponseViewModel>.Success(new AssignFeaturesResponseViewModel(), "Features Assigned To Role successfully");
            else
                return EndPointResponse<AssignFeaturesResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
