using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Cities.BulkDeactivateCity.Orchestrator;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Cities.BulkDeactivateCity
{
    public class BulkDeactivateCityEndPoint : EndpointBase<BulkDeactivateCityRequestViewModel, BulkDeactivateCityResponseViewModel>
    {
        public BulkDeactivateCityEndPoint(EndpointBaseParameters<BulkDeactivateCityRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.BulkDeactivateCity })]
        public async Task<EndPointResponse<BulkDeactivateCityResponseViewModel>> BulkDeactivateCity(BulkDeactivateCityRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<BulkDeactivateCityOrchestrator>());
            if (result.IsSuccess)
                return EndPointResponse<BulkDeactivateCityResponseViewModel>.Success(new BulkDeactivateCityResponseViewModel(), "Cities Deactivated Successfully");
            else
                return EndPointResponse<BulkDeactivateCityResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
