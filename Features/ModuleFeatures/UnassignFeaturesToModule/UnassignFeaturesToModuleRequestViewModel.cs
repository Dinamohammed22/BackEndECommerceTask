using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.ModuleFeatures.AssignFeaturesToModule.Commands;
using KOG.ECommerce.Features.ModuleFeatures.AssignFeaturesToModule;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Features.ModuleFeatures.UnassignFeaturesToModule.Commands;

namespace KOG.ECommerce.Features.ModuleFeatures.UnassignFeaturesToModule
{
    public record UnassignFeaturesToModuleRequestViewModel(string ModuleId, Feature Feature);
    public class UnassignFeaturesToModuleRequestValidator : AbstractValidator<UnassignFeaturesToModuleRequestViewModel>
    {
        public UnassignFeaturesToModuleRequestValidator()
        {
            RuleFor(request => request.ModuleId)
                           .NotEmpty().WithMessage("ModuleId is required.");

            RuleFor(request => request.Feature)
                           .NotNull().WithMessage("Feature is required.");

        }
    }

    public class AssignFeaturesToModuleEndPointProfile : Profile
    {
        public AssignFeaturesToModuleEndPointProfile()
        {
            CreateMap<UnassignFeaturesToModuleRequestViewModel, UnassignFeaturesToModuleCommand>();
        }
    }
}
