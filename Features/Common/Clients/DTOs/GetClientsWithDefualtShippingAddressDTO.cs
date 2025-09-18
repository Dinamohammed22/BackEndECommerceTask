using AutoMapper;
using KOG.ECommerce.Features.Common.ShippingAddresses.DTOs;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Common.Clients.DTOs
{
    public record GetClientsWithDefualtShippingAddressDTO
    (
        string NationalNumber ,
        string Name,
        string Mobile,
        GetAllShippingAddressesDTO ShippingAddresses,
        bool IsActive,
        string UserId
    );

    public class GetClientsWithDefualtShippingAddressDTOProfile : Profile
    {
        public GetClientsWithDefualtShippingAddressDTOProfile()
        {
          
            CreateMap<Client, GetClientsWithDefualtShippingAddressDTO>()
                .ForMember(dest => dest.ShippingAddresses, opt => opt.MapFrom(src =>
                    src.ShippingAddresses
                        .Select(sa => new GetAllShippingAddressesDTO(
                            sa.Governorate.Name,
                            sa.Governorate.ID,
                            sa.City.Name,
                            sa.City.ID,
                            sa.Street,
                            sa.Landmark,
                            sa.Latitude,
                            sa.Longitude,
                            src.Name,
                            src.ID,
                            sa.IsDefualt
                        ))
                        .FirstOrDefault() 
                ));
        }
    }

}
