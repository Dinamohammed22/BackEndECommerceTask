using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.ClientGroups.CreateClientGroup.Commands;

namespace KOG.ECommerce.Features.ClientGroups.CreateClientGroup
{
    public record CreateClientGroupRequestViewModel(string Name, bool TaxExempted);
    public class ClientGroupRequestHandler : AbstractValidator<CreateClientGroupRequestViewModel>
    {
        public ClientGroupRequestHandler()
        {
            RuleFor(request => request.Name)
           .NotEmpty().WithMessage("Name is required.")
           .Length(2, 200).WithMessage("Name must be between 2 and 200 characters.");

        }
    }
    public class CreateClientGroupEndPointRequestProfile : Profile
    {
        public CreateClientGroupEndPointRequestProfile() {
            CreateMap<CreateClientGroupRequestViewModel, CreateClientGroupCommand>();
        }
    }
}
