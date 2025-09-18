using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.RoleFeatures.AssignFeaturesToRole.Commands;
using KOG.ECommerce.Features.RoleFeatures.AssignFeaturesToRole;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Features.RoleFeatures.UnassignFeaturesToRole.Commands;

namespace KOG.ECommerce.Features.RoleFeatures.UnassignFeaturesToRole
{
    public record UnassignFeaturesToRoleRequestViewModel(Role RoleId, Feature Feature);

    public class UnassignFeaturesRequestValidator : AbstractValidator<UnassignFeaturesToRoleRequestViewModel>
    {
        public UnassignFeaturesRequestValidator()
        {
            RuleFor(request => request.RoleId)
                           .NotEmpty().WithMessage("RoleId is required.");

            RuleFor(request => request.Feature)
                           .NotNull().WithMessage("Feature is required.");

        }
    }

    public class UnassignFeaturesEndPointProfile : Profile
    {
        public UnassignFeaturesEndPointProfile()
        {
            CreateMap<UnassignFeaturesToRoleRequestViewModel, UnassignFeaturesToRoleCommand>();
        }
    }
}
