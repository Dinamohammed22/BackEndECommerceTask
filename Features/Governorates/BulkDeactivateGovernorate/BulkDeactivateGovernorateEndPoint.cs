using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Governorates.BulkDeactivateGovernorate.Orchestrator;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Governorates.BulkDeactivateGovernorate
{
    public class BulkDeactivateGovernorateEndPoint : EndpointBase<BulkDeactivateGovernorateRequestViewModel, BulkDeactivateGovernorateResponseViewModel>
    {
        public BulkDeactivateGovernorateEndPoint(EndpointBaseParameters<BulkDeactivateGovernorateRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.BulkDeactivateGovernorate })]
        public async Task<EndPointResponse<BulkDeactivateGovernorateResponseViewModel>> BulkDeactivateGovernorate(BulkDeactivateGovernorateRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<BulkDeactivateGovernorateOrchestrator>());
            if (result.IsSuccess)
                return EndPointResponse<BulkDeactivateGovernorateResponseViewModel>.Success(new BulkDeactivateGovernorateResponseViewModel(), "Governorates Deactivated Successfully");
            else
                return EndPointResponse<BulkDeactivateGovernorateResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
