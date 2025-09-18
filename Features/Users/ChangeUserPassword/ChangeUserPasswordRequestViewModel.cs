using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Users.ChangeUserPassword.Commands;

namespace KOG.ECommerce.Features.Users.ChangeUserPassword
{
    public record ChangeUserPasswordRequestViewModel(string Password, string ConfirmPassword, string? ID);
    public class ChangeUserPasswordRequestValidator:AbstractValidator<ChangeUserPasswordRequestViewModel>
    {
        public ChangeUserPasswordRequestValidator()
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
    public class ChangeUserPasswordRequestProfile:Profile
    {
        public ChangeUserPasswordRequestProfile()
        {
            CreateMap<ChangeUserPasswordRequestViewModel, ChangeUserPasswordCommand>();
        }
    }
}
