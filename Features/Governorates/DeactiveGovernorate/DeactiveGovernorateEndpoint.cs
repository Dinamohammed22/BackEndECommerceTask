using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Features.Governorates.DeactiveGovernorate.Orchestrators;

namespace KOG.ECommerce.Features.Governorates.DeactiveGovernorate
{
    public class DeactiveGovernorateEndpoint : EndpointBase<DeactiveGovernorateRequestViewModel, DeactiveGovernorateResponseViewModel>
    {
        public DeactiveGovernorateEndpoint(EndpointBaseParameters<DeactiveGovernorateRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.DeactiveGovernorate })]
        public async Task<EndPointResponse<DeactiveGovernorateResponseViewModel>> Deactive(DeactiveGovernorateRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<DeactiveGovernorateOrchestrator>());
            if (result.IsSuccess)
                return EndPointResponse<DeactiveGovernorateResponseViewModel>.Success(new DeactiveGovernorateResponseViewModel(), "Governorate Deactivated Successfully");
            else
                return EndPointResponse<DeactiveGovernorateResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
