using AutoMapper;
using KOG.ECommerce.Models.OrderItems;
using KOG.ECommerce.Models.Orders;

namespace KOG.ECommerce.Features.Common.OrderItems.DTOs
{
    public record EditOrderDTO(string ProductId,int Quantity, double ItemPrice);
    public class EditOrderDTOProfile : Profile
    {
        public EditOrderDTOProfile()
        {
            CreateMap<Order, EditOrderDTO>();
            CreateMap<OrderItem, EditOrderDTO>();
            CreateMap<EditOrderDTO, OrderItem>();
        }
    }
}
