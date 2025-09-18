using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Products.GetNewProducts
{
    public class GetNewProductsEndpoint : EndpointBase<GetNewProductsRequestViewModel, GetNewProductsResponseViewModel>
    {
        public GetNewProductsEndpoint(EndpointBaseParameters<GetNewProductsRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
         [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetNewProducts })]
        public async Task<EndPointResponse<IEnumerable<GetNewProductsResponseViewModel>>> GetNewProducts([FromQuery] GetNewProductsRequestViewModel viewModel)
        {

            var result = await _mediator.Send(viewModel.MapOne<GetNewProductsQuery>());

            var response = result.Data.MapList<GetNewProductsResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
            {

                return EndPointResponse<IEnumerable<GetNewProductsResponseViewModel>>
                    .Success(response, "Get New Products successfully.");
            }

            return EndPointResponse<IEnumerable<GetNewProductsResponseViewModel>>
                .Failure(ErrorCode.NotFound);
        }
    }
}
