using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Cities.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Cities.Queries.GetCitiesByGovernorateID
{
    public class GetCitiesByGovernorateIDEndPoint : EndpointBase<GetCitiesByGovernorateIDRequestViewModel, GetCitiesByGovernorateIDResponseViewModel>
    {
        public GetCitiesByGovernorateIDEndPoint(EndpointBaseParameters<GetCitiesByGovernorateIDRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetCitiesbyGovernorateID })]
        public async Task<EndPointResponse<IEnumerable<GetCitiesByGovernorateIDResponseViewModel>>> GetCitiesByGovernorateID([FromQuery] GetCitiesByGovernorateIDRequestViewModel viewModel)
        {

            var result = await _mediator.Send(viewModel.MapOne<GetCitiesByGovernorateIDQuery>());

            IEnumerable<GetCitiesByGovernorateIDResponseViewModel> response = result.Data.MapList<GetCitiesByGovernorateIDResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
                return EndPointResponse<IEnumerable<GetCitiesByGovernorateIDResponseViewModel>>.Success(response, "Cities filtered successfully.");
            else
                return EndPointResponse<IEnumerable<GetCitiesByGovernorateIDResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
