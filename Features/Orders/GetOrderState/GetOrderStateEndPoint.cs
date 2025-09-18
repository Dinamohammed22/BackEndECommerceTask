using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Features.Common.Orders.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Orders.GetOrderState
{
    public class GetOrderStateEndPoint : EndpointBase<GetOrderStateRequestViewModel, GetOrderStateResponseViewModel>
    {
        public GetOrderStateEndPoint(EndpointBaseParameters<GetOrderStateRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetOrderStatus })]
        public async Task<ActionResult<EndPointResponse<GetOrderStateResponseViewModel>>> GetOrderStatus([FromQuery] GetOrderStateRequestViewModel? filter)
        {
            var result = await _mediator.Send(filter.MapOne<GetOrderStateQuery>());
            var response = result.Data.MapOne<GetOrderStateResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
            {
                return EndPointResponse<GetOrderStateResponseViewModel>.Success(response, "Order status returned successfully.");
            }

            return EndPointResponse<GetOrderStateResponseViewModel>.Failure(ErrorCode.NotFound);
        }
    }
}
