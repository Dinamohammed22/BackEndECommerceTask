using AutoMapper;
using KOG.ECommerce.Features.Common.ShippingAddresses.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.ShippingAddresses;

namespace KOG.ECommerce.Features.Common.Clients.DTOs
{
    public class GetClientByIdDTO {
        public string ID { get; set; }
        public string NationalNumber { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string? ClientGroupId { get; set; }
        public string? Phone { get; set; }
        public ClientActivity ClientActivity { get; set; }
        public Religion Religion {  get; set; }
    }

    public class ClientProfileMapping : Profile
    {
        public ClientProfileMapping()
        {
            CreateMap<Client, GetClientByIdDTO>();
                

            CreateMap<ShippingAddress, GetAllShippingAddressesDTO>();
        }
    }

}
