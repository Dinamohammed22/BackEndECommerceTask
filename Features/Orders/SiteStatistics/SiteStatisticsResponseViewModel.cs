using AutoMapper;
using KOG.ECommerce.Features.Common.Orders.DTOs;

namespace KOG.ECommerce.Features.Orders.SiteStatistics
{
    public record SiteStatisticsResponseViewModel(double TotalSales, double TotalSalesThisYear,double TotalOrders,
        double NumberOfProducts, double NumberOfCustomers, double CustomersWaitingApproval);
    public class SiteStatisticsResponseProfile : Profile
    {
        public SiteStatisticsResponseProfile() {
            CreateMap<SiteStatisticsDTO, SiteStatisticsResponseViewModel >();
        }
    }
}
