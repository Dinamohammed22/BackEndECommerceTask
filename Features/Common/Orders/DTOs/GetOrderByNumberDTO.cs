using AutoMapper;
using KOG.ECommerce.Features.Common.OrderItems.DTOs;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.OrderItems;
using KOG.ECommerce.Models.Orders;

namespace KOG.ECommerce.Features.Common.Orders.DTOs
{
    public record GetOrderByNumberDTO
    (
         string OrderNumber ,
         double TotalPrice ,
         double TotalNetPrice,
         OrderStatus Status ,
         DateTime CreatedDate ,
         string OrderId,
         string CustomerName ,
         string Mobile ,
         string Email ,
         List<OrderItemsDTO> OrderItems,
         double TotalLiter
    );
    public class GetOrderByNumberDTOProfile : Profile
    {
        public GetOrderByNumberDTOProfile()
        {
            CreateMap<Order, GetOrderByNumberDTO>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Client.Name))
                .ForMember(dest => dest.Mobile, opt => opt.MapFrom(src => src.Client.Mobile))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Client.Email))
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

            CreateMap<OrderItem, OrderItemsDTO>();  
        }
    }



}
