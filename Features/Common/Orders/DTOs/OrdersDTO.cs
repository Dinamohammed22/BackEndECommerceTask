using AutoMapper;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;

namespace KOG.ECommerce.Features.Common.Orders.DTOs
{
    public class OrdersDTO
    {
        public string ID { get; set; }
        public string OrderNumber { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public double TotalPrice { get; set; }
        public double TotalNetPrice { get; set; }
        public double TotalLiter { get; set; }
        public DateTime CreatedDate { get; set; }
        public ShippingAddressStatus ShippingAddressStatus { get; set; }
        public string ShippingAddressId { get; set; }
    }

    public class OrdersDTOProfile : Profile
    {
        public OrdersDTOProfile()
        {
            CreateMap<Order, OrdersDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Client.Name ?? string.Empty))
                .ForMember(dest => dest.Mobile, opt => opt.MapFrom(src => src.Client.Mobile ?? string.Empty))
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.ShippingAddressStatus, opt => opt.MapFrom(src => src.ShippingAddress.Status))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => Math.Round(src.TotalPrice, 2)))
                .ForMember(dest => dest.TotalNetPrice, opt => opt.MapFrom(src => Math.Round(src.TotalNetPrice, 2)))
                .ForMember(dest => dest.TotalLiter, opt => opt.MapFrom(src => Math.Round(src.TotalLiter, 2)));
        }
    }

}
