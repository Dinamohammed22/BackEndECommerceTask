using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.RoleFeatures.AssignAllFeaturesToRole.Commands;

namespace KOG.ECommerce.Features.RoleFeatures.AssignAllFeaturesToRole
{
    public record AssignAllFeaturesToRoleRequestViewModel();
    public class AssignAllFeaturesToRoleRequestValidator : AbstractValidator<AssignAllFeaturesToRoleRequestViewModel>
    {
        public AssignAllFeaturesToRoleRequestValidator()
        {
        }
    }
    public class AssignAllFeaturesToRoleRequestProfile : Profile
    {
        public AssignAllFeaturesToRoleRequestProfile()
        {
            CreateMap<AssignAllFeaturesToRoleRequestViewModel, AssignAllFeaturesToRoleCommand>();
        }
    }
}
