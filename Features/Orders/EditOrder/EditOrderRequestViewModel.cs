using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.OrderItems;
using KOG.ECommerce.Features.Clients.EditClient.Orchestrators;
using KOG.ECommerce.Features.Orders.EditOrder.Commands;
using KOG.ECommerce.Features.Orders.EditOrder.Orchisterator;
using KOG.ECommerce.Features.ShippingAddresses.EditShippingAddress.Commands;
using KOG.ECommerce.Features.Common.OrderItems.DTOs;
using KOG.ECommerce.Features.Common.ShippingAddresses.Commands;

namespace KOG.ECommerce.Features.Orders.EditOrder
{
    public record EditOrderRequestViewModel (
      string OrderID,
        string OrderNumber,
        List<EditOrderDTO> Items,
        OrderStatus Status,
        string? Comment,
        string ClientID,
        string? NationalNumber,
        string Name,
        string Mobile,
        string? Email,
        string? ClientGroupId,
        string? Phone,
        ClientActivity? ClientActivity,
        string? ShippingAddressID,
        string GovernorateId,
        string CityId,
        string Street,
        string Landmark
        , string BuildingData,
        double Latitude = 0,
        double Longitude = 0
    );
    public class EditOrderRequestViewModelValidator : AbstractValidator<EditOrderRequestViewModel>
    {
        public EditOrderRequestViewModelValidator()
        {
        }
    }
    public class EditOrderRequestViewModelProfile : Profile
    {
        public EditOrderRequestViewModelProfile()
        {
            // Map between EditOrderRequestViewModel and EditOrderOrchisterator
            CreateMap<EditOrderRequestViewModel, EditOrderOrchisterator>();

            // Map between EditOrderOrchisterator and commands
            CreateMap<EditOrderOrchisterator, EditOrderCommand>();

            CreateMap<EditOrderOrchisterator, EditClientOrchestrator>()
             .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ClientID));
            
            CreateMap<EditOrderOrchisterator, CreateShippingAddressInOrderCommand> ();

            CreateMap<EditOrderOrchisterator, EditShippingAddressCommand>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ShippingAddressID));

            // Map between DTOs and domain models
            CreateMap<OrderItem, EditOrderDTO>();
            CreateMap<EditOrderDTO, OrderItem>();
        }
    }
}
