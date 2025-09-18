using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Features.Common.CartProducts.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.CartProducts.GetAllProductAtCartWithPrice
{
    public class GetAllProductAtCartWithPriceEndpoint : EndpointBase<GetAllProductAtCartWithPriceRequestViewModel, GetAllProductAtCartWithPriceResponseViewModel>
    {
        public GetAllProductAtCartWithPriceEndpoint(EndpointBaseParameters<GetAllProductAtCartWithPriceRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetAllProductAtCartWithPrice })]
        public async Task<EndPointResponse<GetAllProductAtCartWithPriceResponseViewModel>> GetAllProductAtCartWithPrice()
        {
            var result = await _mediator.Send(new GetAllProductAtCartWithPriceQuery());

            var response = result.Data.MapOne<GetAllProductAtCartWithPriceResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
            {

                return EndPointResponse<GetAllProductAtCartWithPriceResponseViewModel>
                    .Success(response, "Get Products successfully.");
            }

            return EndPointResponse<GetAllProductAtCartWithPriceResponseViewModel>
                .Failure(ErrorCode.NotFound);
        }
    }
}
