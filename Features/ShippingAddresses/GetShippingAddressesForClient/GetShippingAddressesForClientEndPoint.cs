using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.ShippingAddresses.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.ShippingAddresses.GetShippingAddressesForClient
{
    public class GetShippingAddressesForClientEndPoint : EndpointBase<GetShippingAddressesForClientRequestViewModel, GetShippingAddressesForClientResponseViewModel>
    {
        public GetShippingAddressesForClientEndPoint(EndpointBaseParameters<GetShippingAddressesForClientRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetShippingAddressesForClient })]
        public async Task<ActionResult<EndPointResponse<IEnumerable<GetShippingAddressesForClientResponseViewModel>>>> GetShippingAddressesForClient(
           [FromQuery] GetShippingAddressesForClientRequestViewModel viewModel)
        {
            var result = await _mediator.Send(viewModel.MapOne<GetShippingAddressesForClientQuery>());

            var response = result.Data.MapList<GetShippingAddressesForClientResponseViewModel>();

            if (result.IsSuccess )
                return EndPointResponse<IEnumerable<GetShippingAddressesForClientResponseViewModel>>.Success(response, "ShippingAddress filtered successfully");
            else
                return EndPointResponse<IEnumerable<GetShippingAddressesForClientResponseViewModel>>.Failure(result.ErrorCode);


        }
    }
}
