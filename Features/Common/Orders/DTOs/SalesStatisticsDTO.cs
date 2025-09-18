using AutoMapper;
using KOG.ECommerce.Models.Orders;

namespace KOG.ECommerce.Features.Common.Orders.DTOs
{
    public record SalesStatisticsDTO(double TotalSales, double TotalSalesThisYear, double TotalOrders);
    public class SalesStatisticsDTOProfile : Profile
    {
        public SalesStatisticsDTOProfile()
        {
            CreateMap<Order, SalesStatisticsDTO>();
        }
    }

}
