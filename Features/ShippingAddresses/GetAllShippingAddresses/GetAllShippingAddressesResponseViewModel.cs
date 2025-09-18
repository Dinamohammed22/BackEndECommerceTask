using AutoMapper;
using KOG.ECommerce.Features.Common.ShippingAddresses.DTOs;
using KOG.ECommerce.Features.Common.ShippingAddresses.Queries;

namespace KOG.ECommerce.Features.ShippingAddresses.GetAllShippingAddresses
{
    public record GetAllShippingAddressesResponseViewModel(string GovernorateName, string GovernorateId, string CityName, string CityId, string Street, string Landmark, double Latitude, double Longitude, string ClientName, string ClientId, bool IsDefualt);
    public class GetAllShippingAddressesResponseProfile : Profile
    {
        public GetAllShippingAddressesResponseProfile()
        {
            CreateMap<GetAllShippingAddressesDTO, GetAllShippingAddressesResponseViewModel>();
        }
    }
}
