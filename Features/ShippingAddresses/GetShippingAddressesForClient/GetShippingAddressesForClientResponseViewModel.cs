using AutoMapper;
using KOG.ECommerce.Features.Common.ShippingAddresses.DTOs;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.ShippingAddresses.GetShippingAddressesForClient
{
    public record GetShippingAddressesForClientResponseViewModel(string ID, string GovernorateName, string GovernorateId, string CityName, string CityId, string Street, string Landmark, double Latitude,
        double Longitude, ShippingAddressStatus Status, string BuildingData, bool IsDefualt);
    public class GetShippingAddressesForClientResponseProfile : Profile
    {
        public GetShippingAddressesForClientResponseProfile()
        {
            CreateMap<GetShippingAddressesForClientDTO, GetShippingAddressesForClientResponseViewModel>();
        }
    }
}
