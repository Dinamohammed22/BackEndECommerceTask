using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Orders.PlaceOrderByClient.Orchistrator;

namespace KOG.ECommerce.Features.Orders.PlaceOrderByClient
{
    public record PlaceOrderByClientRequestViewModel(
        string? Comment,
        string? ShippingAddressId, 
        string? GovernorateId,
        string? CityId, 
        string? Street, 
        string? Landmark, 
        double? Latitude,
        double? Longitude,
        bool? IsDefualt,
        string? BuildingData);
    public class PlaceOrderByClientRequestValidator : AbstractValidator<PlaceOrderByClientRequestViewModel>
    {
        public PlaceOrderByClientRequestValidator() { }
    }
    public class PlaceOrderByClientRequestProfile : Profile
    {
        public PlaceOrderByClientRequestProfile() {
            CreateMap<PlaceOrderByClientRequestViewModel, PlaceOrderByClientOrchistrator>();
        }
    }
}
