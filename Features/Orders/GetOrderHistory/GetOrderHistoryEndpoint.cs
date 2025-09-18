using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Features.Common.Orders.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Orders.GetOrderHistory
{
    public class GetOrderHistoryEndpoint : EndpointBase<GetOrderHistoryRequestViewModel, GetOrderHistoryResponseViewModel>
    {
        public GetOrderHistoryEndpoint(EndpointBaseParameters<GetOrderHistoryRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.OrdersHistory })]
        public async Task<ActionResult<EndPointResponse<PagingViewModel<GetOrderHistoryResponseViewModel>>>> OrdersHistory(
        [FromQuery] GetOrderHistoryRequestViewModel filter)
        {
            var result = await _mediator.Send(filter.MapOne<GetOrderHistoryQuery>());
            var response = result.Data.MapPage<GetOrderHistoryDTO, GetOrderHistoryResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
            {
                return EndPointResponse<PagingViewModel<GetOrderHistoryResponseViewModel>>
                    .Success(response, "Order filtered successfully.");
            }

            return EndPointResponse<PagingViewModel<GetOrderHistoryResponseViewModel>>
                .Failure(result.ErrorCode);
        }
    }
}
