using AutoMapper;
using KOG.ECommerce.Features.Common.CartProducts.DTOs;
using KOG.ECommerce.Models.OrderItems;

namespace KOG.ECommerce.Features.Common.OrderItems.DTOs
{
    public record OrderItemDTO(string ProductId, int Quantity);
    public class OrderItemDTOProfile : Profile
    {
        public OrderItemDTOProfile()
        {
            CreateMap<OrderItem, OrderItemDTO>();
            //CreateMap<OrderItemDTO, GetAllProductAtCartDTO>();
        }
    }
}
