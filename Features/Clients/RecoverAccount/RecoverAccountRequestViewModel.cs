using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Clients.ClientRegister.Commands;
using KOG.ECommerce.Features.Clients.ClientRegister;
using KOG.ECommerce.Features.Clients.RecoverAccount.Commands;

namespace KOG.ECommerce.Features.Clients.RecoverAccount
{
    public record RecoverAccountRequestViewModel(string Mobile, string Password, string ConfirmPassword);

    public class RecoverAccountRequestValidator : AbstractValidator<RecoverAccountRequestViewModel>
    {
        public RecoverAccountRequestValidator()
        {
           
            RuleFor(x => x.Mobile)
                .NotEmpty().WithMessage("Mobile number is required.")
                .Matches(@"^\d{10,15}$").WithMessage("Mobile number must be between 10 and 15 digits.");

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .MaximumLength(100).WithMessage("Password cannot exceed 100 characters.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required.")
                .Equal(x => x.Password).WithMessage("Password and Confirm Password must match.");
        }
    }
    public class RecoverAccountEndPointRequestProfile : Profile
    {
        public RecoverAccountEndPointRequestProfile()
        {
            CreateMap<RecoverAccountRequestViewModel, RecoverAccountCommand>();
        }
    }

}
