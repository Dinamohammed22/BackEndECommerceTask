using AutoMapper;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.ShippingAddresses;

namespace KOG.ECommerce.Features.Common.ShippingAddresses.DTOs
{
    public record GetShippingAddressesForClientDTO(
        string ID,
        string GovernorateName,
        string GovernorateId, 
        string CityName,
        string CityId, 
        string Street,
        string Landmark,
        double Latitude,
        double Longitude,
        ShippingAddressStatus Status, 
        string BuildingData,
        bool IsDefualt
    );
    public class GetShippingAddressesForClientDTOProfile : Profile
    {
        public GetShippingAddressesForClientDTOProfile()
        {
            CreateMap<ShippingAddress, GetShippingAddressesForClientDTO>();
        }
    }
}
