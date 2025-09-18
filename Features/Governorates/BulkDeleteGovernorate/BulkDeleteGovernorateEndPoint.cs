using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Governorates.BulkDeleteGovernorate.Orchestrator;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Governorates.BulkDeleteGovernorate
{
    public class BulkDeleteGovernorateEndPoint : EndpointBase<BulkDeleteGovernorateRequestViewModel, BulkDeleteGovernorateResponseViewModel>
    {
        public BulkDeleteGovernorateEndPoint(EndpointBaseParameters<BulkDeleteGovernorateRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpDelete]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.BulkDeleteGovernorate })]
        public async Task<EndPointResponse<BulkDeleteGovernorateResponseViewModel>> BulkDeleteGovernorate(BulkDeleteGovernorateRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<BulkDeleteGovernorateOrchestrator>());
            if (result.IsSuccess)
                return EndPointResponse<BulkDeleteGovernorateResponseViewModel>.Success(new BulkDeleteGovernorateResponseViewModel(), "Governorates Deleted Successfully");
            else
                return EndPointResponse<BulkDeleteGovernorateResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
