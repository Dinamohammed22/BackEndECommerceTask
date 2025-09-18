using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Cities.Queries.GetLisCity;
using KOG.ECommerce.Features.Common.Cities.DTOs;
using KOG.ECommerce.Features.Common.ShippingAddresses.DTOs;
using KOG.ECommerce.Features.Common.ShippingAddresses.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.ShippingAddresses.GetAllShippingAddresses
{
    public class GetAllShippingAddressesEndpoint : EndpointBase<GetAllShippingAddressesRequestViewModel, GetAllShippingAddressesResponseViewModel>
    {
        public GetAllShippingAddressesEndpoint(EndpointBaseParameters<GetAllShippingAddressesRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
       [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetAllShippingAddresses })]
        public async Task<ActionResult<EndPointResponse<PagingViewModel<GetAllShippingAddressesResponseViewModel>>>> GetAll(
           [FromQuery] GetAllShippingAddressesRequestViewModel? filter)
        {

            var result = await _mediator.Send(filter.MapOne<GetAllShippingAddressesQuery>());
            var response = result.Data.MapPage<GetAllShippingAddressesDTO, GetAllShippingAddressesResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
            {

                return EndPointResponse<PagingViewModel<GetAllShippingAddressesResponseViewModel>>
                    .Success(response, "ShippingAddress filtered successfully");
            }

            return EndPointResponse<PagingViewModel<GetAllShippingAddressesResponseViewModel>>
                .Failure(ErrorCode.NotFound);


        }

    }
}
