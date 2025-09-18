using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Features.Orders.GetAllOrders.Orchestrator;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Orders.GetAllOrders
{
    public class GetAllOrdersEndpoint : EndpointBase<GetAllOrdersRequestViewModel, GetAllOrdersResponseViewModel>
    {
        public GetAllOrdersEndpoint(EndpointBaseParameters<GetAllOrdersRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetAllOrders })]
        public async Task<ActionResult<EndPointResponse<PagingViewModel<GetAllOrdersResponseViewModel>>>> GetAllOrders(
          [FromQuery] GetAllOrdersRequestViewModel? filter)
        {
            var result = await _mediator.Send(filter.MapOne<GetAllOrdersOrchestrator>());
            var response = result.Data.MapPage<OrdersDTO, GetAllOrdersResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
            {
                return EndPointResponse<PagingViewModel<GetAllOrdersResponseViewModel>>
                    .Success(response, "Order filtered successfully.");
            }

            return EndPointResponse<PagingViewModel<GetAllOrdersResponseViewModel>>
                .Failure(ErrorCode.NotFound);
        }
    }
}
