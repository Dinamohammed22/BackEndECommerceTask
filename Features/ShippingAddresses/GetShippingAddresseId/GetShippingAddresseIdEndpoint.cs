using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.ShippingAddresses.DTOs;
using KOG.ECommerce.Features.Common.ShippingAddresses.Queries;
using KOG.ECommerce.Features.ShippingAddresses.GetAllShippingAddresses;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.ShippingAddresses.GetShippingAddresseId
{
    public class GetShippingAddresseIdEndpoint : EndpointBase<GetShippingAddresseIdRequestViewModel, GetShippingAddresseIdResponseViewModel>
    {
        public GetShippingAddresseIdEndpoint(EndpointBaseParameters<GetShippingAddresseIdRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetShippingAddresseId })]
        public async Task<ActionResult<EndPointResponse<GetShippingAddresseIdResponseViewModel>>> GetById(
          [FromQuery] GetShippingAddresseIdRequestViewModel filter)
        {

            var result = await _mediator.Send(filter.MapOne<GetShippingAddresseIdQuery>());
            var response = result.Data.MapOne<GetShippingAddresseIdResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
            {

                return EndPointResponse<GetShippingAddresseIdResponseViewModel>
                    .Success(response, "ShippingAddress filtered successfully");
            }

            return EndPointResponse<GetShippingAddresseIdResponseViewModel>
                .Failure(ErrorCode.NotFound);
        }
    }
}
