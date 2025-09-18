using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Products.FilterProducts
{
    public class FilterProductsEndpoint : EndpointBase<FilterProductsRequestViewModel, FilterProductsResponseViewModel>
    {
        public FilterProductsEndpoint(EndpointBaseParameters<FilterProductsRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.FilterProducts })]
        public async Task<EndPointResponse<PagingViewModel<FilterProductsResponseViewModel>>> FilterProducts([FromQuery] FilterProductsRequestViewModel request)
        {
            var result = await _mediator.Send(request.MapOne<FilterProductsQuery>());

            var response = result.Data.MapPage<ProductViewDTO, FilterProductsResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
            {

                return EndPointResponse<PagingViewModel<FilterProductsResponseViewModel>>
                    .Success(response, "Get All Products successfully.");
            }

            return EndPointResponse<PagingViewModel<FilterProductsResponseViewModel>>
                .Failure(ErrorCode.NotFound);
        }
    }
}
