using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Features.Common.Orders.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Orders.GetOrdersStatusPercentage
{
    public class GetOrdersStatusPercentageEndPoint : EndpointBase<GetOrdersStatusPercentageRequestViewModel, GetOrdersStatusPercentageResponseViewModel>
    {
        public GetOrdersStatusPercentageEndPoint(EndpointBaseParameters<GetOrdersStatusPercentageRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetOrdersStatusPercentage })]
        public async Task<ActionResult<EndPointResponse<IEnumerable< GetOrdersStatusPercentageResponseViewModel>>>> GetOrdersStatusPercentage(
          [FromQuery] GetOrdersStatusPercentageRequestViewModel? filter)
        {
            var result = await _mediator.Send(filter.MapOne<GetOrdersStatusPercentageQuery>());
            var response = result.Data.MapList<GetOrdersStatusPercentageResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
            {
                return EndPointResponse<IEnumerable<GetOrdersStatusPercentageResponseViewModel>>.Success(response, "Order status percentage returned successfully.");
            }

            return EndPointResponse<IEnumerable<GetOrdersStatusPercentageResponseViewModel>>.Failure(ErrorCode.NotFound);
        }
    }
}
