using AutoMapper;
using KOG.ECommerce.Features.Common.Clients.DTOs;
using KOG.ECommerce.Features.Common.ShippingAddresses.DTOs;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.ShippingAddresses;

namespace KOG.ECommerce.Features.Clients.GetClientById
{
    public record GetClientByIdResponseViewModel(
       string ID, string NationalNumber, string Name, string Mobile,
        string Email, string? ClientGroupId, string? Phone,
        ClientActivity ClientActivity, Religion Religion
    );
 
    public class GetClientByIdResponseProfile : Profile
    {
        public GetClientByIdResponseProfile()
        {
            CreateMap<GetClientByIdDTO,GetClientByIdResponseViewModel>();
        }
    }

}
