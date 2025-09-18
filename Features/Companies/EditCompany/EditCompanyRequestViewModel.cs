using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Medias.DTOs;
using KOG.ECommerce.Features.Common.Users.EditUser.Commands;
using KOG.ECommerce.Features.Companies.Commands;
using KOG.ECommerce.Features.Companies.EditCompany.Orchestrators;
using KOG.ECommerce.Models.CompanyGovernorates;

namespace KOG.ECommerce.Features.Companies.EditCompany
{
    public record EditCompanyRequestViewModel(
        string ID,
    string? Email,
    double Latitude,
    double Longitude,
    string? Activity,
    string TaxCardID,
    string TaxRegistryNumber,
    string CreditLimit,
    string NID,
    string ManagerName,
    string ManagerMobile,
    bool IsActive,
    string Name,
    string Mobile,
    string GovernorateId,
    string CityId,
    string Address,
    string ClassificationId,
    int MinimumQuantity,
    List<string> CompanyFiles,
    List<string> CompanyImage,
    List<string> GovernorateIds
    );
    public class EditCompanyEndPointRequestValidator : AbstractValidator<EditCompanyRequestViewModel>
    {
        public EditCompanyEndPointRequestValidator()
        {
            RuleFor(request => request.ID)
                .NotEmpty().WithMessage("ID is required.");

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
                .NotEmpty().WithMessage("Tax Registry Number is required.");

            RuleFor(request => request.NID)
                .NotEmpty().WithMessage("NID is required.")
                .Matches(@"^\d{14}$").WithMessage("NID must be exactly 14 digits.");

            RuleFor(request => request.ManagerName)
                .NotEmpty().WithMessage("Manager Name is required.")
                .Length(5, 100).WithMessage("Manager Name must be between 5 and 100 characters.");

            RuleFor(request => request.ManagerMobile)
                .NotEmpty().WithMessage("Manager Mobile is required.")
                .Matches(@"^01[0-9]{9}$").WithMessage("Manager Mobile must be a valid Egyptian number starting with 01.");

            RuleFor(request => request.ClassificationId)
                .NotEmpty().WithMessage("Classification is required.");
        }
    }

    public class EditCompanyEndPointRequestProfile : Profile
    {
        public EditCompanyEndPointRequestProfile()
        {
            CreateMap<EditCompanyRequestViewModel, EditCompanyOrchestrator>();
            CreateMap<EditCompanyOrchestrator, EditUserCommand>();
            CreateMap<EditCompanyOrchestrator, EditCompanyCommand>();
        }
    }
}
