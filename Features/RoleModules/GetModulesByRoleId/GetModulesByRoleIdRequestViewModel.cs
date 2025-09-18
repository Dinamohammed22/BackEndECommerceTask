using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.RoleModules.GetModulesByRoleId.Queries;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.RoleModules.GetModulesByRoleId
{
    public record GetModulesByRoleIdRequestViewModel(Role RoleId);
    public class GetModulesByRoleIdRequestValidator : AbstractValidator<GetModulesByRoleIdRequestViewModel>
    {
        public GetModulesByRoleIdRequestValidator()
        {
        }
    }
    public class GetModulesByRoleIdEndPointProfile : Profile
    {
        public GetModulesByRoleIdEndPointProfile()
        {
            CreateMap<GetModulesByRoleIdRequestViewModel, GetModulesByRoleIdQuery>();
        }
    }

}
