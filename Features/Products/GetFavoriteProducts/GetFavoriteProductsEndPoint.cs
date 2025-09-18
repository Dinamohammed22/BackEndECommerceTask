using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Products.GetFavoriteProducts
{
    public class GetFavoriteProductsEndPoint : EndpointBase<GetFavoriteProductsRequestViewModel, GetFavoriteProductsResponseViewModel>
    {
        public GetFavoriteProductsEndPoint(EndpointBaseParameters<GetFavoriteProductsRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetFavoriteProduct })]
        public async Task<EndPointResponse<IEnumerable<GetFavoriteProductsResponseViewModel>>> GetFavoriteProduct([FromQuery] GetFavoriteProductsRequestViewModel? viewModel)
        {

            var result = await _mediator.Send(viewModel.MapOne<GetFavoriteProductsQuery>());

            var response = result.Data.MapList<GetFavoriteProductsResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
            {

                return EndPointResponse<IEnumerable<GetFavoriteProductsResponseViewModel>>
                    .Success(response, "Get Favorite Products successfully.");
            }

            return EndPointResponse<IEnumerable<GetFavoriteProductsResponseViewModel>>
                .Failure(ErrorCode.NotFound);

        }
    }
}
