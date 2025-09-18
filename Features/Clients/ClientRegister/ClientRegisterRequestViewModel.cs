using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Features.Clients.ClientRegister.Orchestrators;

namespace KOG.ECommerce.Features.Clients.ClientRegister
{
    public record ClientRegisterRequestViewModel(string? NationalNumber, string Name, string Password, string Mobile,
        string GovernorateId, string CityId, string Street,
        string Landmark, double Latitude, double Longitude, string? Email, string ConfirmPassword,
        string? Phone, List<string>? Paths, ClientActivity? ClientActivity, string BuildingData, Religion Religion);

    public class ClientRegisterRequestValidator : AbstractValidator<ClientRegisterRequestViewModel>
    {
        public ClientRegisterRequestValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.");

            
            RuleFor(x => x.Mobile)
                .NotEmpty().WithMessage("Mobile number is required.")
                .Matches(@"^\d{10,15}$").WithMessage("Mobile number must be between 10 and 15 digits.");

            RuleFor(x => x.GovernorateId)
                .NotEmpty().WithMessage("Governorate ID is required.");

            
            RuleFor(x => x.CityId)
                .NotEmpty().WithMessage("City ID is required.");

         
            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("Street is required.")
                .MaximumLength(200).WithMessage("Street cannot exceed 200 characters.");

         
            RuleFor(x => x.Landmark)
                .MaximumLength(200).WithMessage("Landmark cannot exceed 200 characters.");

           
            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90.");

         
            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180.");


            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .MaximumLength(100).WithMessage("Password cannot exceed 100 characters.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required.")
                .Equal(x => x.Password).WithMessage("Password and Confirm Password must match.");
        }
    }
    public class ClientRegisterEndPointRequestProfile : Profile
    {
        public ClientRegisterEndPointRequestProfile()
        {
            CreateMap<ClientRegisterRequestViewModel,ClientRegisterOrchestrator>();
        }
    }
}
