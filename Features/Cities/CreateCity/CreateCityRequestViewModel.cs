using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Cities.CreateCity.Commands;

namespace KOG.ECommerce.Features.Cities.CreateCity
{
    public record CreateCityRequestViewModel(string Name, string GovernorateId, bool IsActive);
    public class CreateCityEndPointRequestValidator : AbstractValidator<CreateCityRequestViewModel>
    {
        public CreateCityEndPointRequestValidator()
        {
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
            CreateMap<CreateCityRequestViewModel, CreateCityCommand>();

        }
    }
}
