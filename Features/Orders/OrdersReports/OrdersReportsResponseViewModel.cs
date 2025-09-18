using AutoMapper;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Orders.OrdersReports
{
    public record OrdersReportsResponseViewModel(
        string ID, 
        string OrderNumber,
        string Name,
        string Mobile,
        OrderStatus OrderStatus,
        double TotalPrice,
        double TotalNetPrice,
        double TotalLiter,
        DateTime CreatedDate,
        ShippingAddressStatus ShippingAddressStatus,
        string ShippingAddressId
    );
    public class OrdersReportsResponseProfile : Profile
    {
        public OrdersReportsResponseProfile()
        {
            CreateMap<OrderReportsDTO, OrdersReportsResponseViewModel>();
        }
    }
}
