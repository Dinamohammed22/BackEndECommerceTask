namespace KOG.ECommerce.Features.Common.Orders.DTOs
{
    public record SiteStatisticsDTO(double TotalSales, double TotalSalesThisYear, double TotalOrders,
        double NumberOfProducts, double NumberOfCustomers, double CustomersWaitingApproval);
   
}
