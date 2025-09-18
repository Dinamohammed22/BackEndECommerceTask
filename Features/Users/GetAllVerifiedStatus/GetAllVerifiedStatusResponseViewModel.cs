using AutoMapper;
using KOG.ECommerce.Features.Common.Users.DTOs;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Users.GetAllVerifiedStatus
{
    public record GetAllVerifiedStatusResponseViewModel(string ID,string Name, string Mobile, Role RoleId, VerifyStatus VerifyStatus, string GovernorateName, string CityName, ClientActivity ClientActivity);
    public class GetAllVerifiedStatusResponseProfile:Profile
    {
        public GetAllVerifiedStatusResponseProfile()
        {
            CreateMap<VerifiedStatusDTO, GetAllVerifiedStatusResponseViewModel>();
        }
    }

}
