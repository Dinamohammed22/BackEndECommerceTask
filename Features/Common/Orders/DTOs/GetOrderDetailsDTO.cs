using AutoMapper;
using KOG.ECommerce.Features.Common.OrderItems.DTOs;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;

namespace KOG.ECommerce.Features.Common.Orders.DTOs
{
    public record GetOrderDetailsDTO
        (
         string OrderID,
         string OrderNumber,
         double TotalPrice,
         double TotalNetPrice,
         int TotalQuantity,
         OrderStatus Status,
         IEnumerable<OrderItemsDTO> Items
        );
    public class GetOrderDetailsDTOProfile:Profile
    {
        public GetOrderDetailsDTOProfile()
        {
            CreateMap<Order, GetOrderDetailsDTO>();
        }
    }
}
