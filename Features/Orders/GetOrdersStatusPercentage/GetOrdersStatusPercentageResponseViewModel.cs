using AutoMapper;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Orders.GetOrdersStatusPercentage
{
    public record GetOrdersStatusPercentageResponseViewModel(OrderStatus OrderStatus, int NumberOfOrders, double Percentage);
    public class GetOrdersStatusPercentageResponseProfile : Profile
    {
        public GetOrdersStatusPercentageResponseProfile()
        {
            CreateMap<GetOrdersStatusPercentageDTO, GetOrdersStatusPercentageResponseViewModel>();
        }
    }

}
