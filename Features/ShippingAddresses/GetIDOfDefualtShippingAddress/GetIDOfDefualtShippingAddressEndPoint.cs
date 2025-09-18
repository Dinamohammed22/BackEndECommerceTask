using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.ShippingAddresses.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.ShippingAddresses.GetIDOfDefualtShippingAddress
{
    public class GetIDOfDefualtShippingAddressEndPoint : EndpointBase<GetIDOfDefualtShippingAddressRequestViewModel, GetIDOfDefualtShippingAddressResponseViewModel>
    {
        public GetIDOfDefualtShippingAddressEndPoint(EndpointBaseParameters<GetIDOfDefualtShippingAddressRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetIDOfDefualtShippingAddress })]
        public async Task<EndPointResponse<GetIDOfDefualtShippingAddressResponseViewModel>> GetIDOfDefualtShippingAddress([FromQuery] GetIDOfDefualtShippingAddressRequestViewModel Request)
        {
            var result = await _mediator.Send(Request.MapOne<GetIDOfDefualtShippingAddressQuery>());
            var response = result.Data.MapOne<GetIDOfDefualtShippingAddressResponseViewModel>();

            if (result.IsSuccess)
                return EndPointResponse<GetIDOfDefualtShippingAddressResponseViewModel>.Success(response, "Shipping Address ID retrived succefully");
            else
                return EndPointResponse<GetIDOfDefualtShippingAddressResponseViewModel>.Failure(result.ErrorCode);
        }
    }

}
