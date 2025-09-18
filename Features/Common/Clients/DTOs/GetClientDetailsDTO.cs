using AutoMapper;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Common.Clients.DTOs
{
    public record GetClientDetailsDTO
   (
        string? NationalNumber, string Name, string Password, string Mobile, 
        string GovernorateId, string CityId, string Street, string GovernorateName, string CityName,
        string Landmark, double Latitude, double Longitude, string? Email,
        string? Phone, ClientActivity? ClientActivity, string BuildingData, ShippingAddressStatus Status,string? Path, Religion Religion
        );
    public class GetUserDetailsDTOProfile : Profile
    {
        public GetUserDetailsDTOProfile()
        {
            CreateMap<Client, GetClientDetailsDTO>();
                 
        }
    }
}
