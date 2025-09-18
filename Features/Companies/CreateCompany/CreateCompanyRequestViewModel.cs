using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Companies.CreateCompany.Orchestrators;

namespace KOG.ECommerce.Features.Companies;

public record CreateCompanyRequestViewModel( string Name, string Mobile, string Address, string GovernorateId,
    string CityId, string? Activity, string TaxCardID, string TaxRegistryNumber,
    string NID, string ManagerName, string ManagerMobile, string ClassificationId,
    string? Email, bool IsActive, double Latitude, double Longitude, string CreditLimit, int MinimumQuantity,
    string Password, string ConfirmPassword, List<string> CompanyImage, List<string> CompanyFiles ,List<string> GovernorateIds);

public class CreateCompanyEndPointRequestValidator : AbstractValidator<CreateCompanyRequestViewModel>
{
    public CreateCompanyEndPointRequestValidator()
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

        RuleFor(request => request.TaxCardID)
            .NotEmpty().WithMessage("TaxCardID is required.")
            .Matches(@"^\d{14}$").WithMessage("TaxCardID must be exactly 14 digits.");

        RuleFor(request => request.TaxRegistryNumber)
            .NotEmpty().WithMessage("TaxRegistryNumber is required.");

        RuleFor(request => request.NID)
            .NotEmpty().WithMessage("NID is required.")
            .Matches(@"^\d{14}$").WithMessage("NID must be exactly 14 digits.");

        RuleFor(request => request.ManagerName)
            .NotEmpty().WithMessage("Manager Name is required.")
            .Length(2, 100).WithMessage("Manager Name must be between 2 and 100 characters.");

        RuleFor(request => request.ManagerMobile)
            .NotEmpty().WithMessage("Manager Mobile is required.")
            .Matches(@"^01[0-9]{9}$").WithMessage("Manager Mobile must be a valid Egyptian number starting with 01.");

        RuleFor(request => request.ClassificationId)
            .NotEmpty().WithMessage("Classification is required.");

        RuleFor(request => request.Email)
            .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email)).WithMessage("Email must be valid.");

        RuleFor(request => request.Latitude)
            .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90.");

        RuleFor(request => request.Longitude)
            .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180.");

        RuleFor(request => request.CreditLimit)
            .NotEmpty().WithMessage("Credit Limit is required.")
            .Matches(@"^\d+(\.\d{1,2})?$").WithMessage("Credit Limit must be a valid number with up to 2 decimal places.");

        RuleFor(request => request.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.");

        RuleFor(request => request.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm Password is required.")
            .Equal(x => x.Password).WithMessage("Passwords do not match.");
    }
}

public class CreateGroupEndPointRequestProfile : Profile
{
    public CreateGroupEndPointRequestProfile()
    {
        CreateMap<CreateCompanyRequestViewModel, CreateCompanyOrchestrator>();
    }
}
