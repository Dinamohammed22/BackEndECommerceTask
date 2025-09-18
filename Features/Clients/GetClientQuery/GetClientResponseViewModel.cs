using AutoMapper;
using KOG.ECommerce.Features.Common.Clients.DTOs;
using KOG.ECommerce.Features.Common.ShippingAddresses.DTOs;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Clients.GetClientQuery
{
    public record GetClientResponseViewModel(string Name,
                                  string NationalNumber,
                                 string Mobile,
                                 string WorkInfo,
                                 List<GetAllShippingAddressesDTO> ShippingAddresses,
                                 bool IsActive);

    public class GetClientResponseProfile : Profile
    {
        public GetClientResponseProfile()
        {
            CreateMap<ClientProfileDTO, GetClientResponseViewModel>();

        }
    }
}
