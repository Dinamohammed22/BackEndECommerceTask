using AutoMapper;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Orders.GetAllOrdersByUserId
{
    public record GetAllOrdersByUserIdResponseViewModel(
        string OrderNumber,
        string Name,
        string Mobile,
        OrderStatus OrderStatus,
        double TotalPrice,
         double TotalLiter,
        DateTime CreatedDate 
    );
    public class GetAllOrdersByUserIdResponseProfile : Profile
    {
        public GetAllOrdersByUserIdResponseProfile()
        {
            CreateMap<OrdersDTO, GetAllOrdersByUserIdResponseViewModel>();
        }
    }
}
