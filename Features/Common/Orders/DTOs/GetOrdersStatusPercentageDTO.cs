using AutoMapper;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;

namespace KOG.ECommerce.Features.Common.Orders.DTOs
{
    public record GetOrdersStatusPercentageDTO(OrderStatus OrderStatus,int NumberOfOrders,double Percentage);
    public class GetOrdersStatusPercentageDTOProfile:Profile
    {
        public GetOrdersStatusPercentageDTOProfile()
        {
            CreateMap<Order, GetOrdersStatusPercentageDTO>();
        }
    }
}
