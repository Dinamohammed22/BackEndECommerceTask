using AutoMapper;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.ShippingAddresses;

namespace KOG.ECommerce.Features.Common.ShippingAddresses.DTOs
{
    public record GetDefualtShippingAddressForClientDTO
        (string ID, 
        string GovernorateName,
        string GovernorateId,
        string CityName,
        string CityId,
        string Street, 
        string Landmark,
        double Latitude, 
        double Longitude ,
        ShippingAddressStatus Status,
        string BuildingData
    );
    public class GetDefualtShippingAddressForClientDTOProfile : Profile
    {
        public GetDefualtShippingAddressForClientDTOProfile()
        {
            CreateMap<ShippingAddress, GetDefualtShippingAddressForClientDTO>();
        }
    }
}
