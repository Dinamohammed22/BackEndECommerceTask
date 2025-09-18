using AutoMapper;
using KOG.ECommerce.Features.Common.Clients.DTOs;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Clients.FilterClient
{
    public record SearchClientResponseViewModel(
        string ID, 
        string Name, 
        bool IsActive, 
        string? ClientGroupName, 
        string Email,
        VerifyStatus VerifyStatus, 
        string NationalNumber, 
        string Mobile , 
        string Phone, 
        int TotalOrders,
        ClientActivity ClientActivity,
        string? Path,
        bool? Deleted,
        Religion Religion
    );
    public class SearchClientResponseProfile : Profile
    {
        public SearchClientResponseProfile()
        {
            CreateMap<SearchClientProfileDTO, SearchClientResponseViewModel>();
        }
    }
}
