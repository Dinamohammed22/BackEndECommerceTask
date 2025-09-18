using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Clients.DTOs;
using KOG.ECommerce.Features.Common.Clients.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Clients.FilterClient
{
    public class SearchClientEndPoint : EndpointBase<SearchClientRequestViewModel, SearchClientResponseViewModel>
    {
        public SearchClientEndPoint(EndpointBaseParameters<SearchClientRequestViewModel> parameters) : base(parameters)
        {
        }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.SearchClient })]
        public async Task<EndPointResponse<PagingViewModel<SearchClientResponseViewModel>>> SearchClient([FromQuery] SearchClientRequestViewModel? viewModel)
        {

            var result = await _mediator.Send(viewModel.MapOne<SearchClientQuery>());

            var response = result.Data.MapPage<SearchClientProfileDTO, SearchClientResponseViewModel>();

            if (result.IsSuccess)
                return EndPointResponse<PagingViewModel<SearchClientResponseViewModel>>.Success(response, "Client filtered successfully.");
            else
                return EndPointResponse<PagingViewModel<SearchClientResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
