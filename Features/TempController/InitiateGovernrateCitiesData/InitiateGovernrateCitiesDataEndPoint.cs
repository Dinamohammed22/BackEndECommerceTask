using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.TempController.InitiateGovernrateCitiesData.Orchistrator;
using KOG.ECommerce.Helpers;

namespace KOG.ECommerce.Features.TempController.InitiateGovernrateCitiesData
{

    public class InitiateGovernrateCitiesDataEndPoint : EndpointBase<InitiateGovernrateCitiesDataRequestViewModel, InitiateGovernrateCitiesDataResponseViewModel>
    {
        public InitiateGovernrateCitiesDataEndPoint(EndpointBaseParameters<InitiateGovernrateCitiesDataRequestViewModel> dependencyParameters) : base(dependencyParameters)
        {

        }
        [HttpPost]
        // [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.InitiatGovernratesAndCities })]
        public async Task<EndPointResponse<InitiateGovernrateCitiesDataResponseViewModel>> InitiatGovernratesAndCities()
        {
            var result = await _mediator.Send(new InitiateGovernrateCitiesDataOrchistrator());
            if (result.IsSuccess)
            {
                return EndPointResponse<InitiateGovernrateCitiesDataResponseViewModel>.Success(new InitiateGovernrateCitiesDataResponseViewModel(), "All Govrnrates and cities in the json fileAdded successfully.");
            }
            return EndPointResponse<InitiateGovernrateCitiesDataResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
