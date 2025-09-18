using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Features.Common.Orders.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;

namespace KOG.ECommerce.Features.Orders.OrdersReports.Orchestrator
{
    public record OrdersReportsOrchestrator(
        string? CustomerName,
        string? CustomerNumber,
        string? OrderNumber,
        OrderStatus? OrderStatus,
        double? TotalPrice,
        DateTime? From,
        DateTime? To,
        int pageIndex = 1,
        int pageSize = 100
     ) : IRequestBase<PagingViewModel<OrderReportsDTO>>;
    public class OrdersReportsOrchestratorHandler : RequestHandlerBase<Order, OrdersReportsOrchestrator, PagingViewModel<OrderReportsDTO>>
    {
        public OrdersReportsOrchestratorHandler(RequestHandlerBaseParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<PagingViewModel<OrderReportsDTO>>> Handle(OrdersReportsOrchestrator request, CancellationToken cancellationToken)
        {
            var role = _userState.RoleID;

            RequestResult<PagingViewModel<OrderReportsDTO>> result = null;

            if (role == Role.Company)
            {
                result = await _mediator.Send(request.MapOne<GetAllCompanyOrdersReportsQuery>());
            }
            else
            {
                result = await _mediator.Send(request.MapOne<OrdersReportsQuery>());
            }

            return RequestResult<PagingViewModel<OrderReportsDTO>>.Success(result.Data);
        }
    }
}

