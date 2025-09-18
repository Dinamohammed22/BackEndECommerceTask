using AutoMapper;
using KOG.ECommerce.Features.Common.Clients.DTOs;
using KOG.ECommerce.Features.Common.ShippingAddresses.DTOs;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Clients.GetClientViewById
{
    public class GetClientViewByIdResponseViewModel
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
    public class GetClientViewByIdResponseProfile : Profile
    {
        public GetClientViewByIdResponseProfile()
        {
            CreateMap<GetClientViewByIdDTO, GetClientViewByIdResponseViewModel>();
        }
    }
}
