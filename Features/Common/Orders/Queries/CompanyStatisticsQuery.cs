using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Clients.Queries;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Orders.Queries
{
    public record CompanyStatisticsQuery(DateTime? From, DateTime? To) : IRequestBase<SiteStatisticsDTO>;

    public class CompanyStatisticsQueryHandler : RequestHandlerBase<Product, CompanyStatisticsQuery, SiteStatisticsDTO>
    {
        public CompanyStatisticsQueryHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<SiteStatisticsDTO>> Handle(CompanyStatisticsQuery request, CancellationToken cancellationToken)
        {
            var from = request.From ?? DateTime.MinValue;
            var to = request.To ?? DateTime.MaxValue;

            var salesStatistics = await _mediator.Send(new CompanySalesStatisticsQuery(from, to));

            //not needed ??
            var clientStatistics = await _mediator.Send(new ClientsOfCompanyStatisticsQuery(from, to));

            var numberOfProducts = await _repository
                .Get(p => p.CreatedDate >= from && p.CreatedDate <= to && p.CompanyId == _userState.UserID)
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
