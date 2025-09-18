using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.ClientGroups.DTOs;
using KOG.ECommerce.Features.Common.ClientGroups.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.ClientGroups.ClientGroupFilterByName
{
    public class ClientGroupFilterByNameEndPoint : EndpointBase<ClientGroupFilterByNameRequestViewModel, ClientGroupFilterByNameResponseViewModel>
    {
        public ClientGroupFilterByNameEndPoint(EndpointBaseParameters<ClientGroupFilterByNameRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.ClientGroupsFilterByName })]
        public async Task<ActionResult<EndPointResponse<PagingViewModel<ClientGroupFilterByNameResponseViewModel>>>> Get(
            [FromQuery] ClientGroupFilterByNameRequestViewModel? filter)
        {

            var result = await _mediator.Send(filter.MapOne<ClientGroupFilterByNameQuery>());
            var response = result.Data.MapPage<ClientGroupProfileDTO, ClientGroupFilterByNameResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
            {

                return EndPointResponse<PagingViewModel<ClientGroupFilterByNameResponseViewModel>>
                    .Success(response, "ClientGroup filtered successfully.");
            }

            return EndPointResponse<PagingViewModel<ClientGroupFilterByNameResponseViewModel>>
                .Failure(ErrorCode.NotFound);
        }
    }
}
