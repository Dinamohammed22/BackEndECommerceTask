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

namespace KOG.ECommerce.Features.Products.FilterProductAdminPage
{
    public class SearchProductEndPoint : EndpointBase<SearchProductRequestViewModel, SearchProductResponseViewModel>
    {
        public SearchProductEndPoint(EndpointBaseParameters<SearchProductRequestViewModel> parameters) : base(parameters)
        {
        }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.FilterProductAdminPage })]
        public async Task<EndPointResponse<PagingViewModel<SearchProductResponseViewModel>>> FilterProductAdminPage([FromQuery] SearchProductRequestViewModel? viewModel)
        {

            var result = await _mediator.Send(viewModel.MapOne<SearchProductQuery>());

            var response = result.Data.MapPage<SearchProductProfileDTO, SearchProductResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
            {

                return EndPointResponse<PagingViewModel<SearchProductResponseViewModel>>
                    .Success(response, "Products filtered successfully.");
            }

            return EndPointResponse<PagingViewModel<SearchProductResponseViewModel>>
                .Failure(ErrorCode.NotFound);

        }
    }
}
