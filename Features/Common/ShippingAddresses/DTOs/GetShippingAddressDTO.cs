using AutoMapper;
using KOG.ECommerce.Models.ShippingAddresses;

namespace KOG.ECommerce.Features.Common.ShippingAddresses.DTOs
{
    public record GetShippingAddressDTO(string ID, string GovernorateName, string GovernorateId, string CityName, string CityId, string Street, string Landmark, double Latitude, double Longitude);
    public class GetShippingAddressDTOProfile : Profile
    {
        public GetShippingAddressDTOProfile()
        {
            CreateMap<ShippingAddress, GetShippingAddressDTO>();
        }
    }

}
