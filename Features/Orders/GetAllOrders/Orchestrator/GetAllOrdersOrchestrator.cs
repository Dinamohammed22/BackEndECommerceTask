using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Features.Common.Orders.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;

namespace KOG.ECommerce.Features.Orders.GetAllOrders.Orchestrator
{
    public record GetAllOrdersOrchestrator(
        string? CustomerName,
        string? CustomerNumber,
        string? OrderNumber,
        OrderStatus? OrderStatus,
        double? TotalPrice,
        DateTime? From,
        DateTime? To,
        int pageIndex = 1,
        int pageSize = 100
     ) : IRequestBase<PagingViewModel<OrdersDTO>>;
    public class GetAllOrdersOrchestratorHandler : RequestHandlerBase<Order, GetAllOrdersOrchestrator, PagingViewModel<OrdersDTO>>
    {
        public GetAllOrdersOrchestratorHandler(RequestHandlerBaseParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<PagingViewModel<OrdersDTO>>> Handle(GetAllOrdersOrchestrator request, CancellationToken cancellationToken)
        {
            var role = _userState.RoleID;

            RequestResult<PagingViewModel<OrdersDTO>> result = null;

            if (role == Role.Company)
            {
                result = await _mediator.Send(request.MapOne<GetAllCompanyOrdersQuery>());
            }
            else
            {
                result = await _mediator.Send(request.MapOne<GetAllOrdersQuery>());
            }

            return RequestResult<PagingViewModel<OrdersDTO>>.Success(result.Data);
        }
    }
}

