using AutoMapper;
using KOG.ECommerce.Models.Modules;

namespace KOG.ECommerce.Features.Common.RoleModules.GetModulesByRoleId.DTOs
{
    public record GetModulesByRoleIdProfileDTO(string ID, string Name);
    public class GetModulesByRoleIdProfile : Profile
    {
        public GetModulesByRoleIdProfile()
        {
            CreateMap<Module, GetModulesByRoleIdProfileDTO>();
        }
    }
}
