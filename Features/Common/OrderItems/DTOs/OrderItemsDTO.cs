using AutoMapper;
using KOG.ECommerce.Features.Common.CartProducts.DTOs;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Models.OrderItems;
using KOG.ECommerce.Models.Orders;

namespace KOG.ECommerce.Features.Common.OrderItems.DTOs
{
    public record OrderItemsDTO(
        string ProductId, 
        int Quantity,
        double ItemPrice,
        double Price, 
        double NetPrice,
        string Name, 
        double ItemLiter, 
        double Liter,
        int ItemPoint,
        int Point,
        string? Path ,
        string CompanyName, string CompanyId
    );

    public class GetOrderByNumberDTOProfile : Profile
    {
        public GetOrderByNumberDTOProfile()
        {
            // This works because AutoMapper can match constructor parameter names
            CreateMap<Order, GetOrderByNumberDTO>()
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));  // Map the collection
            CreateMap<OrderItem, OrderItemsDTO>()
               .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Product.Company.Name))
               .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Product.Company.ID.ToString()));

        }
    }


}
