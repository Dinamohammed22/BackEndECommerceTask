using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Products.SearchProductBrandCategoryNames
{
    public class SearchProductBrandCategoryNamesEndpoint : EndpointBase<SearchProductBrandCategoryNamesRequestViewModel, SearchProductBrandCategoryNamesResponseViewModel>
    {
        public SearchProductBrandCategoryNamesEndpoint(EndpointBaseParameters<SearchProductBrandCategoryNamesRequestViewModel> dependencyCollection)
            : base(dependencyCollection)
        {
        }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.SearchProductBrandCategoryNames })]
        public async Task<EndPointResponse<IEnumerable<SearchProductBrandCategoryNamesResponseViewModel>>> SearchProductBrandCategoryNames([FromQuery] SearchProductBrandCategoryNamesRequestViewModel viewModel)
        {
            var query = viewModel.MapOne<SearchProductBrandCategoryNamesQuery>();

            var result = await _mediator.Send(query);

            var response = result.Data.MapList<SearchProductBrandCategoryNamesResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
            {
                return EndPointResponse<IEnumerable<SearchProductBrandCategoryNamesResponseViewModel>>
                    .Success(response, "Get Names successfully.");
            }

            return EndPointResponse<IEnumerable<SearchProductBrandCategoryNamesResponseViewModel>>
                .Failure(result.ErrorCode);
        }
    }
}
