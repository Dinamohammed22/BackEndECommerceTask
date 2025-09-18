using AutoMapper;
using KOG.ECommerce.Features.Common.OrderItems.DTOs;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;

namespace KOG.ECommerce.Features.Common.Orders.DTOs
{
    public record GetOrderByIDDTO
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
        ShippingAddressStatus  ShippingAddressStatus,
        string BuildingData,
        Religion Religion,
        double Latitude = 0,
        double Longitude = 0
    );
    public class GetOrderByIDDTOProfile:Profile
    {
        public GetOrderByIDDTOProfile()
        {
            CreateMap<Order, GetOrderByIDDTO>();
        }
    }
}
