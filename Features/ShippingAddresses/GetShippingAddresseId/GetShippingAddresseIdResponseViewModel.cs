using AutoMapper;
using KOG.ECommerce.Features.Common.ShippingAddresses.DTOs;

namespace KOG.ECommerce.Features.ShippingAddresses.GetShippingAddresseId
{
    public record GetShippingAddresseIdResponseViewModel(string GovernorateName, string GovernorateId, string CityName, string CityId,
        string Street, string Landmark, double Latitude, double Longitude, bool IsDefualt, string BuildingData);
    public class GetShippingAddresseIdResponseProfile : Profile
    {
        public GetShippingAddresseIdResponseProfile()
        {
            CreateMap<GetShippingAddresseIdDTO, GetShippingAddresseIdResponseViewModel>();
        }
    }
}
