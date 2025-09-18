using AutoMapper;
using KOG.ECommerce.Models.Orders;

namespace KOG.ECommerce.Features.Common.Orders.DTOs
{
    public record PlaceOrderDTO
    (
        string ID,
        string OrderNumber
    );
    public class PlaceOrderDTOProfile : Profile
    {
        public PlaceOrderDTOProfile()
        {
            CreateMap<Order, PlaceOrderDTO>();
        }
    }
}
