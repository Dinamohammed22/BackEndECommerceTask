using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Features.Common.Orders.Queries;
using KOG.ECommerce.Features.Orders.GetAllOrders;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Orders.GetAllOrdersByUserId
{
    public class GetAllOrdersByUserIdEndPoint : EndpointBase<GetAllOrdersByUserIdRequestViewModel, GetAllOrdersByUserIdResponseViewModel>
    {
        public GetAllOrdersByUserIdEndPoint(EndpointBaseParameters<GetAllOrdersByUserIdRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetAllOrdersByUserId })]
        public async Task<ActionResult<EndPointResponse<PagingViewModel<GetAllOrdersByUserIdResponseViewModel>>>> GetAllOrdersByUserId(
          [FromQuery] GetAllOrdersByUserIdRequestViewModel? request)
        {
            var result = await _mediator.Send(request.MapOne<GetAllOrdersByUserIdQuery>());
            var response = result.Data.MapPage<OrdersDTO, GetAllOrdersByUserIdResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
            {
                return EndPointResponse<PagingViewModel<GetAllOrdersByUserIdResponseViewModel>>
                    .Success(response, "User Orders History fetched successfully.");
            }

            return EndPointResponse<PagingViewModel<GetAllOrdersByUserIdResponseViewModel>>
                .Failure(ErrorCode.NotFound);
        }
    }
}
