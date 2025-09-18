using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Governorates.BulkActivateGovernorate.Orchestrator;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Governorates.BulkActivateGovernorate
{
    public class BulkActivateGovernorateEndPoint : EndpointBase<BulkActivateGovernorateRequestViewModel, BulkActivateGovernorateResponseViewModel>
    {
        public BulkActivateGovernorateEndPoint(EndpointBaseParameters<BulkActivateGovernorateRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.BulkActivateGovernorate })]
        public async Task<EndPointResponse<BulkActivateGovernorateResponseViewModel>> BulkActivateGovernorate(BulkActivateGovernorateRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<BulkActivateGovernorateOrchestrator>());
            if (result.IsSuccess)
                return EndPointResponse<BulkActivateGovernorateResponseViewModel>.Success(new BulkActivateGovernorateResponseViewModel(), "Governorates Activated Successfully");
            else
                return EndPointResponse<BulkActivateGovernorateResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
