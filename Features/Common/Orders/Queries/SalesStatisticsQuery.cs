using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Orders.Queries
{
    public record SalesStatisticsQuery(DateTime From, DateTime To) : IRequestBase<SalesStatisticsDTO>;

    public class SalesStatisticsQueryHandler : RequestHandlerBase<Order, SalesStatisticsQuery, SalesStatisticsDTO>
    {
        public SalesStatisticsQueryHandler(RequestHandlerBaseParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<SalesStatisticsDTO>> Handle(SalesStatisticsQuery request, CancellationToken cancellationToken)
        {
           var totalSales = await _repository.Get(o => o.Status == OrderStatus.Completed && o.CreatedDate >= request.From && o.CreatedDate <= request.To)
                .SumAsync(o => o.TotalNetPrice);

            var currentYear = DateTime.Now.Year;
            var totalSalesThisYear = await _repository.Get(o => o.Status == OrderStatus.Completed && o.CreatedDate.Year == currentYear)
                .SumAsync(o => o.TotalNetPrice, cancellationToken);

            var totalOrders = await _repository.Get(o => o.Status == OrderStatus.Completed && o.CreatedDate >= request.From && o.CreatedDate <= request.To)
                .CountAsync();

            var salesStatistics = new SalesStatisticsDTO(
                TotalSales: totalSales,
                TotalSalesThisYear: totalSalesThisYear,
                TotalOrders:totalOrders
            );

            return RequestResult<SalesStatisticsDTO>.Success(salesStatistics);
        }
    }
}
