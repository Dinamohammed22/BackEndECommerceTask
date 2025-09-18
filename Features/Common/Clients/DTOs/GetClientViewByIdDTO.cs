using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Features.Common.ShippingAddresses.DTOs;
using AutoMapper;
using KOG.ECommerce.Models.Clients;

namespace KOG.ECommerce.Features.Common.Clients.DTOs
{
    public class GetClientViewByIdDTO
    {
        public string ID { get; set; }
        public string? NationalNumber { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string? Phone { get; set; }
        public Religion Religion { get; set; }
        public string? ClientGroupName { get; set; }
        public string? Email { get; set; }
        public double Wallet { get; set; }
        public int NumberOfPoints { get; set; }
        public ClientActivity? ClientActivity { get; set; }
        public int NumberOfOrder { get; set; }
        public ICollection<GetShippingAddresseIdDTO> ShippingAddresses { get; set; }
        public bool IsActive { get; set; }
        public Role RoleId { get; set; }
        public VerifyStatus VerifyStatus { get; set; }
        public string? RejectReason { get; set; }
        public string? JobTitle { get; set; }
        public bool Deleted { get; set; }
    }
    public class GetClientViewByIdDTOProfile : Profile
    {
        public GetClientViewByIdDTOProfile()
        {
            CreateMap<Client, GetClientViewByIdDTO>()
               .ForMember(dest => dest.ClientGroupName, opt => opt.MapFrom(src => src.ClientGroup.Name ?? string.Empty))
               .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.User.IsActive))
               .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.User.RoleId))
               .ForMember(dest => dest.VerifyStatus, opt => opt.MapFrom(src => src.User.VerifyStatus))
               .ForMember(dest => dest.RejectReason, opt => opt.MapFrom(src => src.User.RejectReason ?? string.Empty))
               .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.User.JobTitle ?? string.Empty));

        }
    }
}
