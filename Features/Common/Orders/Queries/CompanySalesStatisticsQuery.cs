using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Orders.Queries
{
    public record CompanySalesStatisticsQuery(DateTime From, DateTime To) : IRequestBase<SalesStatisticsDTO>;

    public class CompanySalesStatisticsQueryHandler : RequestHandlerBase<Order, CompanySalesStatisticsQuery, SalesStatisticsDTO>
    {
        public CompanySalesStatisticsQueryHandler(RequestHandlerBaseParameters<Order> requestParameters) : base(requestParameters)
        {
        }
        public async override Task<RequestResult<SalesStatisticsDTO>> Handle(CompanySalesStatisticsQuery request, CancellationToken cancellationToken)
        {
            var userId = _userState.UserID;
            var currentYear = DateTime.Now.Year;

            var orders = await _repository.Get(o =>
                    o.Status == OrderStatus.Completed &&
                    o.CreatedDate >= request.From &&
                    o.CreatedDate <= request.To)
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                .Where(o => o.OrderItems.Any(oi => oi.Product.CompanyId == userId))
                .ToListAsync(cancellationToken);

            var totalSales = orders.Sum(o => o.TotalNetPrice);
            var totalOrders = orders.Count;
            var totalSalesThisYear = orders
                .Where(o => o.CreatedDate.Year == currentYear)
                .Sum(o => o.TotalNetPrice);

            var salesStatistics = new SalesStatisticsDTO(
                TotalSales: totalSales,
                TotalSalesThisYear: totalSalesThisYear,
                TotalOrders: totalOrders
            );

            return RequestResult<SalesStatisticsDTO>.Success(salesStatistics);
        }

    }
}
