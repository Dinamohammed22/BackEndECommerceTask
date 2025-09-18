using AutoMapper;
using KOG.ECommerce.Features.Common.RoleModules.GetModulesByRoleId.DTOs;

namespace KOG.ECommerce.Features.RoleModules.GetModulesByRoleId
{
    public record GetModulesByRoleIdResponseViewModel(string ID,string Name );
    public class GetModulesByRoleIdResponseProfile : Profile
    {
        public GetModulesByRoleIdResponseProfile()
        {
            CreateMap<GetModulesByRoleIdProfileDTO, GetModulesByRoleIdResponseViewModel>();
        }
    }
}
