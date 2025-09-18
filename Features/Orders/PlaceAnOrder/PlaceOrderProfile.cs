using AutoMapper;
using KOG.ECommerce.Features.Common.Discounts.DTOs;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Features.Common.ShippingAddresses.Commands;
using KOG.ECommerce.Features.Orders.PlacedAnOrder.Commands;
using KOG.ECommerce.Features.Orders.PlacedAnOrder.Orchestrators;

namespace KOG.ECommerce.Features.Orders.PlaceAnOrder
{
    public record PlaceOrderDto(
        string ClientID,
        string Comment,
        string ShippingAddressId,
        IEnumerable<GetPriceOfProductsDTO> Products,
        GetAllDiscountsDTO CurrentDiscount,
        string? DiscountId,
        double TotalPrice,
        double TotalnetPrice,
        int TotalQuantity
    );
    public class PlaceOrderProfile : Profile
    {
        public PlaceOrderProfile()
        {
            CreateMap<PlaceOrderDto, PlaceAnOrderCommand>();
            CreateMap<PlaceOrderOrchestrator, CreateShippingAddressInOrderCommand>();
        }
    }
}
