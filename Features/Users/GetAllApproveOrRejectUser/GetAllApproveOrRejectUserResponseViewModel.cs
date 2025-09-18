using AutoMapper;
using KOG.ECommerce.Features.Common.Users.DTOs;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Users.GetAllApproveOrRejectUser
{
    public record GetAllApproveOrRejectUserResponseViewModel(string ID,string Name, string Mobile, Role RoleId, VerifyStatus VerifyStatus, string GovernorateName, string CityName, ClientActivity ClientActivity);
    public class GetAllApproveOrRejectUserResponseProfile : Profile
    {
        public GetAllApproveOrRejectUserResponseProfile()
        {
            CreateMap<VerifiedStatusDTO, GetAllApproveOrRejectUserResponseViewModel>();
        }
    }
}
