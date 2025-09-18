using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.ModuleFeatures.AssignFeaturesToModule.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.ModuleFeatures.AssignFeaturesToModule
{
    public class AssignFeaturesToModuleEndPoint : EndpointBase<AssignFeaturesToModuleRequestViewModel, AssignFeaturesToModuleResponseViewModel>
    {
        public AssignFeaturesToModuleEndPoint(EndpointBaseParameters<AssignFeaturesToModuleRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPost]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.AssignFeaturesToModule })]
        public async Task<EndPointResponse<AssignFeaturesToModuleResponseViewModel>> AssignFeaturesToModule(AssignFeaturesToModuleRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<AssignFeaturesToModuleCommand>());
            if (result.IsSuccess)
                return EndPointResponse<AssignFeaturesToModuleResponseViewModel>.Success(new AssignFeaturesToModuleResponseViewModel(), "Features Assigned To Module successfully");
            else
                return EndPointResponse<AssignFeaturesToModuleResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
