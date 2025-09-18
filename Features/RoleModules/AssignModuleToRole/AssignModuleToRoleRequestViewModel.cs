using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.RoleModules.AssignModuleToRole.Commands;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.RoleModules.AssignModuleToRole
{
    public record AssignModuleToRoleRequestViewModel(Role RoleId, List<string> ModulesId);
    public class AssignModuleToRoleRequestValidator : AbstractValidator<AssignModuleToRoleRequestViewModel>
    {
        public AssignModuleToRoleRequestValidator()
        {
            RuleFor(request => request.RoleId)
                           .NotEmpty().WithMessage("RoleId is required.");

            RuleFor(request => request.ModulesId)
                           .NotEmpty().WithMessage("Features is required.");

        }
    }

    public class AssignModuleToRoleEndPointProfile : Profile
    {
        public AssignModuleToRoleEndPointProfile()
        {
            CreateMap<AssignModuleToRoleRequestViewModel, AssignModuleToRoleCommand>();
        }
    }
}
