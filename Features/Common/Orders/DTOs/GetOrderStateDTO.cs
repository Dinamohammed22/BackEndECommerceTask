using AutoMapper;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;

namespace KOG.ECommerce.Features.Common.Orders.DTOs
{
    public record GetOrderStateDTO(OrderStatus Status, DateTime? StatusDate);

    public class GetOrderStateDTOProfile : Profile
    {
        public GetOrderStateDTOProfile() 
        {
            CreateMap<Order, GetOrderStateDTO>();
        }
    }
}
