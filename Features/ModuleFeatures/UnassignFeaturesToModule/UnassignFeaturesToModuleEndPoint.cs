using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.ModuleFeatures.UnassignFeaturesToModule.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.ModuleFeatures.UnassignFeaturesToModule
{
    public class UnassignFeaturesToModuleEndPoint : EndpointBase<UnassignFeaturesToModuleRequestViewModel, UnassignFeaturesToModuleResponseViewModel>
    {
        public UnassignFeaturesToModuleEndPoint(EndpointBaseParameters<UnassignFeaturesToModuleRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpDelete]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.UnassignFeaturesfromModule })]
        public async Task<EndPointResponse<UnassignFeaturesToModuleResponseViewModel>> UnassignFeaturesToModule(UnassignFeaturesToModuleRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<UnassignFeaturesToModuleCommand>());
            if (result.IsSuccess)
                return EndPointResponse<UnassignFeaturesToModuleResponseViewModel>.Success(new UnassignFeaturesToModuleResponseViewModel(), "Features Unassigned To Module successfully");
            else
                return EndPointResponse<UnassignFeaturesToModuleResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
