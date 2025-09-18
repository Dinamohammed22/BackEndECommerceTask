using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.ClientGroups.EditClientGroup.Commands;

namespace KOG.ECommerce.Features.ClientGroups.EditClientGroup
{
    public record EditClientGroupRequestViewModel(string Id, string Name, bool TaxExempted);
    public class EditClientGroupRequestHandler : AbstractValidator<EditClientGroupRequestViewModel>
    {
        public EditClientGroupRequestHandler() {
            RuleFor(request => request.Id)
       .NotEmpty().WithMessage("Id is required.");
            RuleFor(request => request.Name)
           .NotEmpty().WithMessage("Name is required.")
           .Length(2, 200).WithMessage("Name must be between 2 and 200 characters.");

        }
    }
    public class EditClientGroupEndPointRequestProfile : Profile
    {
        public EditClientGroupEndPointRequestProfile()
        {
            CreateMap<EditClientGroupRequestViewModel, EditClientGroupCommand>();
        }
    }
}
