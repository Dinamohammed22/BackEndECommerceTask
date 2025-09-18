using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Features.Orders.OrdersReports.Orchestrator;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Orders.OrdersReports
{
    public class OrdersReportsEndpoint : EndpointBase<OrdersReportsRequestViewModel, OrdersReportsResponseViewModel>
    {
        public OrdersReportsEndpoint(EndpointBaseParameters<OrdersReportsRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.OrdersReports })]
        public async Task<ActionResult<EndPointResponse<PagingViewModel<OrdersReportsResponseViewModel>>>> OrdersReports(
          [FromQuery] OrdersReportsRequestViewModel? filter)
        {
            var result = await _mediator.Send(filter.MapOne<OrdersReportsOrchestrator>());
            var response = result.Data.MapPage<OrderReportsDTO, OrdersReportsResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
            {
                return EndPointResponse<PagingViewModel<OrdersReportsResponseViewModel>>
                    .Success(response, "Order filtered successfully.");
            }

            return EndPointResponse<PagingViewModel<OrdersReportsResponseViewModel>>
                .Failure(ErrorCode.NotFound);
        }
    }
}
