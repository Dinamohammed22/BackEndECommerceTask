using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Cities.BulkDeleteCity.Orchestrator;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Middlewares;

namespace KOG.ECommerce.Features.Cities.BulkDeleteCity
{
    public class BulkDeleteCityEndPoint : EndpointBase<BulkDeleteCityRequestViewModel, BulkDeleteCityResponseViewModel>
    {
        public BulkDeleteCityEndPoint(EndpointBaseParameters<BulkDeleteCityRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpDelete]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.BulkDeleteCity })]
        public async Task<EndPointResponse<BulkDeleteCityResponseViewModel>> BulkDeleteCity(BulkDeleteCityRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<BulkDeleteCityOrchestrator>());
            if (result.IsSuccess)
                return EndPointResponse<BulkDeleteCityResponseViewModel>.Success(new BulkDeleteCityResponseViewModel(), "Cities Deleted Successfully");
            else
                return EndPointResponse<BulkDeleteCityResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
