using AutoMapper;
using KOG.ECommerce.Models.OrderItems;

namespace KOG.ECommerce.Features.Common.OrderItems.DTOs
{
    public class OrderItemWithItemNameDTO
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public double ItemPrice { get; set; }
        public String CompanyId { get; set; }
        public String CompanyName { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public double ItemLiter { get; set; }
        public double Liter { get; set; }
        public int ItemPoint { get; set; }
        public int Point { get; set; }
        public int MinimumQuantity { get; set; }
        public int MaximumQuantity { get; set; }
    }
    public class OrderItemWithItemNameDTOProfile : Profile
    {
        public OrderItemWithItemNameDTOProfile()
        {
            CreateMap<OrderItem, OrderItemWithItemNameDTO>()
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Product.CompanyId))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Product.Company.Name))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.MinimumQuantity, opt => opt.MapFrom(src => src.Product.MinimumQuantity))
                .ForMember(dest => dest.MaximumQuantity, opt => opt.MapFrom(src => src.Product.MaximumQuantity));
        }
    }

}
