using AutoMapper;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Orders.GetAllOrders
{
    public record GetAllOrdersResponseViewModel(
        string ID, 
        string OrderNumber,
        string Name,
        string Mobile,
        OrderStatus OrderStatus,
        double TotalPrice,
        double TotalNetPrice,
        double TotalLiter,
        DateTime CreatedDate,
        ShippingAddressStatus ShippingAddressStatus,
        string ShippingAddressId
    );
    public class GetAllOrdersResponseProfile : Profile
    {
        public GetAllOrdersResponseProfile()
        {
            CreateMap<OrdersDTO, GetAllOrdersResponseViewModel>();
        }
    }
}
