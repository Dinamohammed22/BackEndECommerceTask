using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Classifications.Queries;
using KOG.ECommerce.Helpers;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Classifications.GetClassificationsNames
{
    public class GetClassificationsNamesEndpoint : EndpointBase<GetClassificationsNamesRequestViewModel, GetClassificationsNamesResponseViewModel>
    {
        public GetClassificationsNamesEndpoint(EndpointBaseParameters<GetClassificationsNamesRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        public async Task<ActionResult<EndPointResponse<IEnumerable<GetClassificationsNamesResponseViewModel>>>> GetClassificationsNames()
        {
            var result = await _mediator.Send(new GetClassificationsNamesQuery());
            var response = result.Data.MapList<GetClassificationsNamesResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
            {
                return EndPointResponse<IEnumerable<GetClassificationsNamesResponseViewModel>>
                    .Success(response, "Classifications Names and Products Number get successfully.");
            }

            return EndPointResponse<IEnumerable<GetClassificationsNamesResponseViewModel>>
                .Failure(result.ErrorCode);
        }
    }
}
