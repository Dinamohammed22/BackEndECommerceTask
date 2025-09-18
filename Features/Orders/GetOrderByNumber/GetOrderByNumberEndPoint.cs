using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Orders.Queries;
using KOG.ECommerce.Features.Orders.GetOrderByNumber.Orchestrators;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Orders.GetOrderByNumber
{
    public class GetOrderByNumberEndPoint : EndpointBase<GetOrderByNumberRequestViewModel, GetOrderByNumberResponseViewModel>
    {
        public GetOrderByNumberEndPoint(EndpointBaseParameters<GetOrderByNumberRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetOrderByNumber })]
        public async Task<ActionResult<EndPointResponse<GetOrderByNumberResponseViewModel>>> GetOrderByNumber(
    [FromQuery] GetOrderByNumberRequestViewModel viewModel)
        {
            var result = await _mediator.Send(viewModel.MapOne<GetOrderByNumberOrchestrator>());

            if (result.IsSuccess && result.Data != null)
            {
                var response = result.Data.MapOne<GetOrderByNumberResponseViewModel>();
                return EndPointResponse<GetOrderByNumberResponseViewModel>.Success(response, "Get Order successfully.");
            }
            else
            {
                return EndPointResponse<GetOrderByNumberResponseViewModel>.Failure(result.ErrorCode);
            }
        }
    }
}
