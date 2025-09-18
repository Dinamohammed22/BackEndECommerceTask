using AutoMapper;
using KOG.ECommerce.Features.Common.ShippingAddresses.DTOs;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.ShippingAddresses;

namespace KOG.ECommerce.Features.Common.Clients.DTOs
{
    public class ClientProfileDTO
    {
        public string NationalNumber { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string WorkInfo { get; set; }
        public List<GetAllShippingAddressesDTO> ShippingAddresses { get; set; }
        public bool IsActive { get; set; }
    }

    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientProfileDTO>()
                .ForMember(dest => dest.ShippingAddresses, opt => opt.MapFrom(src => src.ShippingAddresses.Select(sa => new GetAllShippingAddressesDTO(
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
                )).ToList()));
        }
    }
}
