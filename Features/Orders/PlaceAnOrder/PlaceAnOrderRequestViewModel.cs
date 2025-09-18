using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.CartProducts.DTOs;
using KOG.ECommerce.Features.Orders.PlacedAnOrder.Commands;
using KOG.ECommerce.Features.Orders.PlacedAnOrder.Orchestrators;
using KOG.ECommerce.Features.Orders.PlaceOrderByAdmin.Orchistrator;
using KOG.ECommerce.Features.ShippingAddresses.CreateShippingAddress.Commands;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Orders.PlacedAnOrder
{
    public record PlaceAnOrderRequestViewModel(OrderStatus OrderStatus,
        string ClientID,
        string? Comment,
        string? ShippingAddressId,
        string? GovernorateId,
        string? CityId,
        string? Street,
        string? Landmark,
        double? Latitude,
        double? Longitude,
        bool? IsDefualt,
        string? BuildingData,
        ShippingAddressStatus? Status,
        IEnumerable<GetAllProductAtCartDTO> cartProductsResult);
    public class PlacedAnOrderRequestValidator : AbstractValidator<PlaceAnOrderRequestViewModel>
    {
        public PlacedAnOrderRequestValidator()
        {
        }
    }
    public class PlacedAnOrderRequestProfile : Profile
    {
        public PlacedAnOrderRequestProfile() {
            CreateMap<PlaceAnOrderRequestViewModel, PlaceOrderOrchestrator>();
            CreateMap<PlaceOrderOrchestrator, PlaceAnOrderCommand>();

        }
    }
}
