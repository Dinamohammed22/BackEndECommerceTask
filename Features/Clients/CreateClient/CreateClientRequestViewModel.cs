using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Features.Clients.CreateClient.Orchestrators;

namespace KOG.ECommerce.Features.Clients.CreateClient
{
    public record CreateClientRequestViewModel(string? NationalNumber, string Name, string Password, string Mobile, string GovernorateId, string CityId, string Street,
        string Landmark, double Latitude, double Longitude, string? Email, string ConfirmPassword,
        string? ClientGroupId, string? Phone, List<string>? Paths, ClientActivity? ClientActivity, string BuildingData, Religion Religion);
    public class CreateClientRequestValidator : AbstractValidator<CreateClientRequestViewModel>
    {
        public CreateClientRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(1, 100).WithMessage("Name must be between 1 and 100 characters.");

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .MaximumLength(100).WithMessage("Password cannot exceed 100 characters.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required.")
                .Equal(x => x.Password).WithMessage("Password and Confirm Password must match.");
        }
    }
    public class CreateClientEndPointRequestProfile : Profile
    {
        public CreateClientEndPointRequestProfile()
        {
            CreateMap<CreateClientRequestViewModel, CreateClientOrchestrator>();
        }
    }
}
