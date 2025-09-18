using AutoMapper;
using FluentValidation;

using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Features.ModuleFeatures.AssignFeaturesToModule.Commands;

namespace KOG.ECommerce.Features.ModuleFeatures.AssignFeaturesToModule
{
    public record AssignFeaturesToModuleRequestViewModel(string ModuleId, Feature Feature);
    public class AssignFeaturesToModuleRequestValidator : AbstractValidator<AssignFeaturesToModuleRequestViewModel>
    {
        public AssignFeaturesToModuleRequestValidator()
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
            CreateMap<AssignFeaturesToModuleRequestViewModel, AssignFeaturesToModuleCommand>();
        }
    }


}
