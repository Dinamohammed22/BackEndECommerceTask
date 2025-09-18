using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Features.Cities.ActiveCity.Orchestrators;

namespace KOG.ECommerce.Features.Cities.ActiveCity
{
    public class ActiveCityEndPoint : EndpointBase<ActiveCityRequestViewModel, ActiveCityResponseViewModel>
    {
        public ActiveCityEndPoint(EndpointBaseParameters<ActiveCityRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.ActiveCity })]
        public async Task<EndPointResponse<ActiveCityResponseViewModel>> Active(ActiveCityRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<ActiveCityOrchestrator>());
            if (result.IsSuccess)
                return EndPointResponse<ActiveCityResponseViewModel>.Success(new ActiveCityResponseViewModel(), "City Activated Successfully");
            else
                return EndPointResponse<ActiveCityResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
