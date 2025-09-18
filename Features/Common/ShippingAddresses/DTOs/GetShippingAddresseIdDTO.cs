using AutoMapper;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.ShippingAddresses;

namespace KOG.ECommerce.Features.Common.ShippingAddresses.DTOs
{
    public record GetShippingAddresseIdDTO(
        string GovernorateName,
        string GovernorateId, 
        string CityName, 
        string CityId,
        string Street,
        string Landmark,
        double Latitude, 
        double Longitude, 
        bool IsDefualt, 
        string BuildingData,
        ShippingAddressStatus Status
    );
    public class GetShippingAddresseIdDTOProfile : Profile
    {
        public GetShippingAddresseIdDTOProfile()
        {
            CreateMap<ShippingAddress, GetShippingAddresseIdDTO>()
                .ForMember(dest => dest.GovernorateName, opt => opt.MapFrom(src => src.Governorate.Name ?? string.Empty))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name ?? string.Empty));
;
        }
    }
}
