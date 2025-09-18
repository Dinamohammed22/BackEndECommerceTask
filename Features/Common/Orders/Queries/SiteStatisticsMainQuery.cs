using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Data;
using KOG.ECommerce.Features.Common.Clients.Queries;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Orders.Queries
{
    public record SiteStatisticsMainQuery(DateTime? From, DateTime? To) : IRequestBase<SiteStatisticsDTO>;

    public class SiteStatisticsMainQueryHandler : RequestHandlerBase<Product, SiteStatisticsMainQuery, SiteStatisticsDTO>
    {
        public SiteStatisticsMainQueryHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<SiteStatisticsDTO>> Handle(SiteStatisticsMainQuery request, CancellationToken cancellationToken)
        {
            var from = request.From ?? DateTime.MinValue;
            var to = request.To ?? DateTime.MaxValue;

            var salesStatistics = await _mediator.Send(new SalesStatisticsQuery(from, to));

            var clientStatistics = await _mediator.Send(new ClientsStatisticsQuery(from, to));

            var numberOfProducts = await _repository
                .Get(p => p.CreatedDate >= from && p.CreatedDate <= to)
                .CountAsync(cancellationToken);

            var siteStatistics = new SiteStatisticsDTO(
                TotalSales: salesStatistics.Data.TotalSales,
                TotalSalesThisYear: salesStatistics.Data.TotalSalesThisYear,
                TotalOrders: salesStatistics.Data.TotalOrders,
                NumberOfProducts: numberOfProducts,
                NumberOfCustomers: clientStatistics.Data.NumberOfCustomers,
                CustomersWaitingApproval: clientStatistics.Data.CustomersWaitingApproval
            );

            return RequestResult<SiteStatisticsDTO>.Success(siteStatistics);
        }
    }
}
