using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Cities.BulkActivateCity.Orchestrator;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Cities.BulkActivateCity
{
    public class BulkActivateCityEndPoint : EndpointBase<BulkActivateCityRequestViewModel, BulkActivateCityResponseViewModel>
    {
        public BulkActivateCityEndPoint(EndpointBaseParameters<BulkActivateCityRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.BulkActivateCity })]
        public async Task<EndPointResponse<BulkActivateCityResponseViewModel>> BulkActivateCity(BulkActivateCityRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<BulkActivateCityOrchestrator>());
            if (result.IsSuccess)
                return EndPointResponse<BulkActivateCityResponseViewModel>.Success(new BulkActivateCityResponseViewModel(), "Cities Activated Successfully");
            else
                return EndPointResponse<BulkActivateCityResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
