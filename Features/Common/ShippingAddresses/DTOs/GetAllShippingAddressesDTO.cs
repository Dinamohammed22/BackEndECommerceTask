using AutoMapper;
using KOG.ECommerce.Models.ShippingAddresses;

namespace KOG.ECommerce.Features.Common.ShippingAddresses.DTOs
{
    public record GetAllShippingAddressesDTO(
        string GovernorateName,
        string GovernorateId,
        string CityName,
        string CityId ,
        string Street,
        string Landmark ,
        double Latitude,
        double Longitude, 
        string ClientName,
        string ClientId,
        bool IsDefualt
    );
    public class GetAllShippingAddressesDTOProfile : Profile
    {
        public GetAllShippingAddressesDTOProfile() {
            CreateMap<ShippingAddress, GetAllShippingAddressesDTO>();
        }
    }

}
