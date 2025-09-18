using AutoMapper;
using KOG.ECommerce.Features.Common.OrderItems.DTOs;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Orders.GetOrderByID
{
    public record GetOrderByIDResponseViewModel
     (

        string OrderID,
        string OrderNumber,
        List<OrderItemWithItemNameDTO> Items,
        OrderStatus Status,
        ClientActivity ClientActivity,
        string Comment,
        string ClientID,
        string NationalNumber,
        string Name,
        string Mobile,
        string Email,
        string? ClientGroupId,
        string? Phone,
        string ShippingAddressID,
        string GovernorateId,
        string CityId,
        string Street,
        string Landmark,
        double TotalLiter,
        ShippingAddressStatus ShippingAddressStatus,
        string BuildingData,
        Religion Religion,
        double Latitude = 0,
        double Longitude = 0
    );
    public class GetOrderByIDResponseProfile:Profile
    {
        public GetOrderByIDResponseProfile()
        {
            CreateMap<GetOrderByIDDTO, GetOrderByIDResponseViewModel>();
        }
    }
}
