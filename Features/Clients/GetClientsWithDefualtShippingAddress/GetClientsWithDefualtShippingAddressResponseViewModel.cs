using AutoMapper;
using KOG.ECommerce.Features.Common.Clients.DTOs;
using KOG.ECommerce.Features.Common.ShippingAddresses.DTOs;

namespace KOG.ECommerce.Features.Clients.GetClientsWithDefualtShippingAddress
{
    public record GetClientsWithDefualtShippingAddressResponseViewModel(string NationalNumber,
        string Name,
        string Mobile,
        GetAllShippingAddressesDTO ShippingAddresses,
        bool IsActive,
        string UserId);
    public class GetClientsWithDefualtShippingAddressResponseProfile : Profile
    {
        public GetClientsWithDefualtShippingAddressResponseProfile()
        {
            CreateMap<GetClientsWithDefualtShippingAddressDTO, GetClientsWithDefualtShippingAddressResponseViewModel>();
        }
    }
}
