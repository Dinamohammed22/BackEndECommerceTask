using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.CartProducts.DTOs;
using KOG.ECommerce.Features.Orders.PlacedAnOrder.Orchestrators;
using KOG.ECommerce.Features.Orders.PlaceOrderByAdmin.Orchistrator;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Orders.PlaceOrderByAdmin
{
    public record PlaceOrderByAdminRequestViewModel
    (
    //client data
        string? NationalNumber,
        string? Name,
        string? Password,
        string? ConfirmPassword,
        string Mobile,
        string? Email,
        string? Phone,
        string? ClientGroupId,
        Religion? Religion,
        List<string>? Paths,
        ClientActivity? ClientActivity,
        //shipping adress data
        string? GovernorateId,
        string? CityId,
        string? Street,
        string? Landmark,
        string? ShippingAddressId,
        //order data
        string? Comment,
        string? BuildingData,
        //default value data
        IEnumerable<GetAllProductAtCartDTO> cartProductsResult,
        bool IsDefualt = false,
        bool NotifyCustomer = true,
        double Latitude = 0.0,
        double Longitude = 0.0
    );

    public class PlaceOrderByAdminRequestValidator : AbstractValidator<PlaceOrderByAdminRequestViewModel>
    {
        public PlaceOrderByAdminRequestValidator()
        {
        }
    }

    public class PlaceOrderByAdminRequestProfile : Profile
    {
        public PlaceOrderByAdminRequestProfile()
        {
            CreateMap<PlaceOrderByAdminRequestViewModel, PlaceOrderByAdminOrchisterator>();
        }
    }
}
