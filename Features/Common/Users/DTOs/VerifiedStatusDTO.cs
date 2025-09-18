using AutoMapper;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Companies;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Features.Common.Users.DTOs
{
    public record VerifiedStatusDTO( string ID,string Name,string Mobile , Role RoleId, VerifyStatus VerifyStatus,string GovernorateName, string CityName, ClientActivity? ClientActivity);
    public class VerifiedStatusProfile : Profile
    {
        public VerifiedStatusProfile()
        {
            CreateMap<User, VerifiedStatusDTO>()
                .ConstructUsing(src => new VerifiedStatusDTO(
                    src.ID,
                    src.Name,
                    src.Mobile,
                    src.RoleId,
                    src.VerifyStatus,
                    src.Client.ShippingAddresses.FirstOrDefault().Governorate.Name,
                    src.Client.ShippingAddresses.FirstOrDefault().City.Name,
                    src.Client.ClientActivity
                ));
        }
    }

}
