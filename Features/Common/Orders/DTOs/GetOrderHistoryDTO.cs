using AutoMapper;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;

namespace KOG.ECommerce.Features.Common.Orders.DTOs
{
    public class GetOrderHistoryDTO
    {
        public string ID { get; set; }
        public string OrderNumber { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public OrderStatus Status { get; set; }
        public double TotalNetPrice { get; set; }
        public double TotalPrice { get; set; }
        public int TotalQuantity { get; set; }
        public DateTime Date { get; set; }
    }
        public class GetOrderHistoryProfile : Profile
    {
        public GetOrderHistoryProfile()
        {
            CreateMap<Order, GetOrderHistoryDTO>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Client.Name ?? string.Empty))
               .ForMember(dest => dest.Mobile, opt => opt.MapFrom(src => src.Client.Mobile ?? string.Empty))
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }
    }
}
