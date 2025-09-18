using AutoMapper;
using KOG.ECommerce.Features.Common.OrderItems.DTOs;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Features.Common.Orders.Queries;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Orders.GetOrderDetails
{
    public record GetOrderDetailsResponseViewModel
         (
         string OrderID,
         string OrderNumber,
         double TotalPrice,
         double TotalNetPrice,
         int TotalQuantity,
         OrderStatus Status,
         IEnumerable<OrderItemsDTO> Items
        );
    public class GetOrderDetailsResponseProfile:Profile
    {
        public GetOrderDetailsResponseProfile()
        {
            CreateMap<GetOrderDetailsDTO, GetOrderDetailsResponseViewModel>();
        }

    }
}
