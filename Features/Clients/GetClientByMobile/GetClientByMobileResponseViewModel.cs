using AutoMapper;
using KOG.ECommerce.Features.Common.Clients.DTOs;
using KOG.ECommerce.Features.Common.ShippingAddresses.DTOs;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Clients.GetClientByMobile
{
    public record GetClientByMobileResponseViewModel
    (string? NationalNumber,
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
        Religion Religion
    );
    public class GetClientByNationalNumberResponseProfile : Profile
    {
        public GetClientByNationalNumberResponseProfile()
        {
            CreateMap<GetClientByNationalNumberDTO, GetClientByMobileResponseViewModel>();
        }
    }
}
