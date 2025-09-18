using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Clients.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Clients.GetClientsWithDefualtShippingAddress
{
    public class GetClientsWithDefualtShippingAddressEndpoint : EndpointBase<GetClientsWithDefualtShippingAddressRequestViewModel, GetClientsWithDefualtShippingAddressResponseViewModel>
    {
        public GetClientsWithDefualtShippingAddressEndpoint(EndpointBaseParameters<GetClientsWithDefualtShippingAddressRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetClientsWithDefualtShippingAddress })]
        public async Task<EndPointResponse<IEnumerable<GetClientsWithDefualtShippingAddressResponseViewModel>>> GetList()
        {

            var result = await _mediator.Send(new GetClientsWithDefualtShippingAddressQuery());


            var response = result.Data.MapList<GetClientsWithDefualtShippingAddressResponseViewModel>();

            if (result.IsSuccess)
                return EndPointResponse<IEnumerable<GetClientsWithDefualtShippingAddressResponseViewModel>>.Success(response, "Client filtered successfully.");
            else
                return EndPointResponse<IEnumerable<GetClientsWithDefualtShippingAddressResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
