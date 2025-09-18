using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Users.ActivateUser.Commands;
using KOG.ECommerce.Features.Users.ApproveUser.Commands;
using KOG.ECommerce.Features.Users.ApproveUser.Orchestrators;

namespace KOG.ECommerce.Features.Users.ApproveUser
{
    public record ApproveUserRequestViewModel(string ID);
    public class ApproveUserRequestValidator : AbstractValidator<ApproveUserRequestViewModel>
    {
        public ApproveUserRequestValidator() {
            RuleFor(request => request.ID)
            .NotEmpty().WithMessage("ID is required.")
            .Length(1, 100).WithMessage("ID must be between 1 and 100 characters.");

        }
    }
    public class ApproveUserRequestProfile : Profile
    {
        public ApproveUserRequestProfile()
        {
            CreateMap<ApproveUserRequestViewModel, ApproveUserOrchestrator>();
            CreateMap<ApproveUserOrchestrator, ApproveUserCommand>();
            CreateMap<ApproveUserOrchestrator, ActivateUserCommand>();
        }
    }
}
