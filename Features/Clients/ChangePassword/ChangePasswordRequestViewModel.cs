using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Clients.ChangePassword.Orchestrators;

namespace KOG.ECommerce.Features.Clients.ChangePassword
{
    public record ChangePasswordRequestViewModel(string Password, string ConfirmPassword, string? ID);
    public class ChangePasswordRequestValidator:AbstractValidator<ChangePasswordRequestViewModel>
    {
        public ChangePasswordRequestValidator()
        {
            RuleFor(x => x.Password)
         .NotEmpty().WithMessage("Password is required.")
         .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
         .MaximumLength(100).WithMessage("Password cannot exceed 100 characters.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required.")
                .Equal(x => x.Password).WithMessage("Password and Confirm Password must match.");
        }
    }
    public class ChangePasswordRequestProfile:Profile
    {
        public ChangePasswordRequestProfile()
        {
            CreateMap<ChangePasswordRequestViewModel, ChangePasswordOrchestrator>();
        }
    }
}
