using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Governorates.CreateGovernorate.Commands;

namespace KOG.ECommerce.Features.Governorates.CreateGovernorate;

public record CreateGovernorateRequestViewModel(string Name, string GovernorateCode, bool IsActive);

public class CreateGovernorateRequestValidator : AbstractValidator<CreateGovernorateRequestViewModel>
{
    public CreateGovernorateRequestValidator()
    {
        RuleFor(request => request.Name).NotEmpty().Length(2, 200);
        RuleFor(request => request.GovernorateCode).NotEmpty().Length(2, 200);

    }
}
public class CreateGovernorateEndPointRequestProfile : Profile
{
    public CreateGovernorateEndPointRequestProfile()
    {
        CreateMap<CreateGovernorateRequestViewModel, CreateGovernorateCommand>();
    }
}