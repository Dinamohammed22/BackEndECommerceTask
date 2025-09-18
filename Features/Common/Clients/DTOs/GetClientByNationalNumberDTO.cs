using AutoMapper;
using KOG.ECommerce.Features.Common.ShippingAddresses.DTOs;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Common.Clients.DTOs
{
    public record GetClientByNationalNumberDTO
   (
        string? NationalNumber,
        string Name,
        string Mobile,
        GetShippingAddressDTO ShippingAddresses,
        bool IsActive,
        string UserId,
        string? Email,
        string? Phone,
        string? ClientGroupId,
        string? ClientGroupName,
        string? Path,
        VerifyStatus Status,
        Religion Religion);
    public class GetClientByNationalNumberProfileDTO : Profile
    {
        public GetClientByNationalNumberProfileDTO()
        {
            CreateMap<Client, GetClientByNationalNumberDTO>();
        }
    }
}
