using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Cities.CreateCity.Commands;
using KOG.ECommerce.Features.Cities.CreateCity;
using KOG.ECommerce.Features.Cities.EditCity.Commands;

namespace KOG.ECommerce.Features.Cities.EditCity
{
    public record EditCityRequestViewModel(string Id,string Name, string GovernorateId, bool IsActive);
    public class EditCityEndPointRequestValidator : AbstractValidator<EditCityRequestViewModel>
    {
        public EditCityEndPointRequestValidator()
        {
            RuleFor(request => request.Id)
           .NotEmpty().WithMessage("ID is required.")
           .Length(1, 100).WithMessage("ID must be between 1 and 50 characters.");

            RuleFor(request => request.GovernorateId)
             .NotEmpty().WithMessage("GovernorateId is required.")
             .Length(1, 100).WithMessage("GovernorateId must be between 1 and 50 characters.");

            RuleFor(request => request.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 200).WithMessage("Name must be between 2 and 200 characters.");
        }
    }
    public class CreateGroupEndPointRequestProfile : Profile
    {
        public CreateGroupEndPointRequestProfile()
        {
            CreateMap<EditCityRequestViewModel, EditCityCommand>();

        }
    }
}
