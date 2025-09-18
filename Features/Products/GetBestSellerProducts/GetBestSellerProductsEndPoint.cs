using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Products.GetBestSellerProducts
{
    public class GetBestSellerProductsEndPoint : EndpointBase<GetBestSellerProductsRequest, GetBestSellerProductsResponse>
    {
        public GetBestSellerProductsEndPoint(EndpointBaseParameters<GetBestSellerProductsRequest> parameters) : base(parameters)
        {
        }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetBestSellerProducts })]
        public async Task<EndPointResponse<IEnumerable<GetBestSellerProductsResponse>>> GetBestSellerProducts([FromQuery] GetBestSellerProductsRequest request)
        {
            var result = await _mediator.Send(request.MapOne<GetBestSellerProductsQuery>());

            var response = result.Data.MapList<GetBestSellerProductsResponse>();

            if (result.IsSuccess && result.Data != null)
            {

                return EndPointResponse<IEnumerable<GetBestSellerProductsResponse>>
                    .Success(response, "Get best seller Products successfully.");
            }

            return EndPointResponse<IEnumerable<GetBestSellerProductsResponse>>
                .Failure(ErrorCode.NotFound);
        }
    }
}
