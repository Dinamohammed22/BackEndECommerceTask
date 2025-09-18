using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Features.Common.ClientGroups.Queries;
using KOG.ECommerce.Helpers;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.ClientGroups.GetClientGroupByID
{
    public class GetClientGroupByIDEndPoint : EndpointBase<GetClientGroupByIDRequestViewModel, GetClientGroupByIDResponseViewModel>
    {
        public GetClientGroupByIDEndPoint(EndpointBaseParameters<GetClientGroupByIDRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetClientGroupsByID })]
        public async Task<ActionResult<EndPointResponse<GetClientGroupByIDResponseViewModel>>> Get(
         [FromQuery] GetClientGroupByIDRequestViewModel filter)
        {

            var result = await _mediator.Send(filter.MapOne<GetClientGroupByIDQuery>());
            GetClientGroupByIDResponseViewModel response = result.Data.MapOne<GetClientGroupByIDResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
            {

                return EndPointResponse<GetClientGroupByIDResponseViewModel>
                    .Success(response, "ClientGroup filtered successfully.");
            }

            return EndPointResponse<GetClientGroupByIDResponseViewModel>
                .Failure(ErrorCode.NotFound);


        }
    }
}
