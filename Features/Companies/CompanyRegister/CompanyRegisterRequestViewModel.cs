using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Companies.CompanyRegister.Orchestrators;

namespace KOG.ECommerce.Features.Companies.CompanyRegister
{
    public record CompanyRegisterRequestViewModel(
    string Name, string Mobile, string Address, string GovernorateId,
    string CityId, string? Email, bool IsActive, string Password,
    string ConfirmPassword, List<string> GovernorateIds);
    public class CompanyRegisterRequestValidator : AbstractValidator<CompanyRegisterRequestViewModel>
    {
        public CompanyRegisterRequestValidator()
        {
            RuleFor(request => request.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(2, 200).WithMessage("Name must be between 2 and 200 characters.");

            RuleFor(request => request.Mobile)
                .NotEmpty().WithMessage("Mobile is required.")
                .Matches(@"^01[0-9]{9}$").WithMessage("Mobile must be a valid Egyptian number starting with 01.");

            RuleFor(request => request.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(300).WithMessage("Address cannot exceed 300 characters.");

            RuleFor(request => request.GovernorateId)
                .NotEmpty().WithMessage("Governorate is required.");

            RuleFor(request => request.CityId)
                .NotEmpty().WithMessage("City is required.");

            RuleFor(request => request.Email)
                .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email)).WithMessage("Email must be valid.");

            RuleFor(request => request.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.");

            RuleFor(request => request.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required.")
                .Equal(x => x.Password).WithMessage("Passwords do not match.");
        }
    }
    public class CompanyRegisterRequestProfile:Profile
    {
        public CompanyRegisterRequestProfile()
        {
            CreateMap<CompanyRegisterRequestViewModel, CompanyRegisterOrchestrator>();
        }
    }
}
