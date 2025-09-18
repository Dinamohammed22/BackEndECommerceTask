using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Data;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KOG.ECommerce.Features.Common.Orders.Queries
{
    public record GetOrdersStatusPercentageQuery(DateTime? From, DateTime? To) : IRequestBase<IEnumerable<GetOrdersStatusPercentageDTO>>;
    public class GetOrdersStatusPercentageQueryHandler : RequestHandlerBase<Order, GetOrdersStatusPercentageQuery, IEnumerable<GetOrdersStatusPercentageDTO>>
    {
        public GetOrdersStatusPercentageQueryHandler(RequestHandlerBaseParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<GetOrdersStatusPercentageDTO>>> Handle(GetOrdersStatusPercentageQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateExtensions.PredicateExtensions.Begin<Order>(true)
                .And(o => !request.From.HasValue || o.CreatedDate >= request.From.Value)
                .And(o => !request.To.HasValue || o.CreatedDate <= request.To.Value);

            var userId = _userState.UserID;

            IQueryable<Order> query;

            if (_userState.RoleID == Role.Company)
            {
                query = _repository.Get(predicate)
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                    .Where(o => o.OrderItems.Any(oi => oi.Product.CompanyId == userId))
                    .AsQueryable();
            }
            else
            {
                query = _repository.Get(predicate);
            }

            var orderStatusesQuery = query.Select(o => o.Status);

            var totalOrders = await orderStatusesQuery.CountAsync(cancellationToken);

            var orderCounts = await orderStatusesQuery
                .GroupBy(status => status)
                .Select(group => new { Status = group.Key, Count = group.Count() })
                .ToDictionaryAsync(x => x.Status, x => x.Count, cancellationToken);

            var result = Enum.GetValues<OrderStatus>()
                .Select(status => new GetOrdersStatusPercentageDTO(
                    status,
                    orderCounts.TryGetValue(status, out var count) ? count : 0,
                    totalOrders > 0 ? Math.Round((orderCounts.TryGetValue(status, out count) ? count * 100.0 / totalOrders : 0), 2) : 0.0
                ))
                .ToList();

            return RequestResult<IEnumerable<GetOrdersStatusPercentageDTO>>.Success(result);
        }



    }
}
