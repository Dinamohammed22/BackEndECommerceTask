using AutoMapper;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Common.Clients.DTOs
{
    public record SearchClientProfileDTO
    {
        public string ID { get; init; }
        public string Name { get; init; }
        public bool IsActive { get; init; }
        public string? ClientGroupName { get; init; }
        public string Email { get; init; }
        public VerifyStatus VerifyStatus { get; init; }
        public string NationalNumber { get; init; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public int TotalOrders { get; init; }
        public ClientActivity ClientActivity { get; init; }
        public string? Path { get; set; } // Now mutable
        public bool? Deleted { get; set; }
        public Religion Religion {  get; set; }
    }

    public class SearchClientProfile : Profile
    {
        public SearchClientProfile()
        {
            CreateMap<Client, SearchClientProfileDTO>()
                .ForMember(dest => dest.ClientGroupName, opt => opt.MapFrom(src => src.ClientGroup.Name))
                .ForMember(dest => dest.VerifyStatus, opt => opt.MapFrom(src => src.User.VerifyStatus))
                .ForMember(dest => dest.TotalOrders, opt => opt.MapFrom(src => src.Orders.Count))
               .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.User.IsActive));

        }
    }
}
