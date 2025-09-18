using AutoMapper;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Orders.GetOrderState
{
    public record GetOrderStateResponseViewModel(OrderStatus Status, DateTime? StatusDate);
    public class GetOrderStateResponseProfile : Profile
    {
        public GetOrderStateResponseProfile()
        {
            CreateMap<GetOrderStateDTO, GetOrderStateResponseViewModel>();
        }
    }
}
