using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Clients.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Clients.GetClientQuery
{
    public class GetClientEndPoint : EndpointBase<GetClientRequestViewModel, GetClientResponseViewModel>
    {
        public GetClientEndPoint(EndpointBaseParameters<GetClientRequestViewModel> parameters) : base(parameters)
        {
        }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetClientList })]
        public async Task<EndPointResponse<IEnumerable<GetClientResponseViewModel>>> GetList()
        {

            var result = await _mediator.Send(new GetClientsQuery());


            var response = result.Data.MapList<GetClientResponseViewModel>();

            if (result.IsSuccess)
                return EndPointResponse<IEnumerable<GetClientResponseViewModel>>.Success(response, "Client filtered successfully.");
            else
                return EndPointResponse<IEnumerable<GetClientResponseViewModel>>.Failure(result.ErrorCode);

        }
    }

}
