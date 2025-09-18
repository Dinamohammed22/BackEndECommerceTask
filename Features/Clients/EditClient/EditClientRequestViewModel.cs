using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Clients.EditClient.Commands;
using KOG.ECommerce.Features.Clients.EditClient.Orchestrators;
using KOG.ECommerce.Features.Common.Users.EditUser.Commands;
using KOG.ECommerce.Features.Orders.EditOrder.Commands;
using KOG.ECommerce.Features.Orders.EditOrder.Orchisterator;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Clients.EditClient
{
    public record EditClientRequestViewModel(string ID, string? NationalNumber, string Name,
        string Mobile, string? Email, string? ClientGroupId, string? Phone,
        ClientActivity? ClientActivity);
    public class EditClientRequestValidator : AbstractValidator<EditClientRequestViewModel>
    {
        public EditClientRequestValidator() {
           

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.");


            RuleFor(x => x.Mobile)
                .NotEmpty().WithMessage("Mobile number is required.")
                .Matches(@"^\d{10,15}$").WithMessage("Mobile number must be between 10 and 15 digits.");
        }
    }
    public class EditClientRequestProfile : Profile
    {
        public EditClientRequestProfile() {
            CreateMap<EditClientRequestViewModel, EditClientOrchestrator>();
            CreateMap<EditClientOrchestrator, EditUserCommand>();
            CreateMap<EditClientOrchestrator, EditClientCommand>();

            // Map between EditOrderOrchisterator and commands
            CreateMap<EditOrderOrchisterator, EditOrderCommand>();
        }
    }
}
