using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Companies.DeactiveCompany.Commands;
using KOG.ECommerce.Features.Companies.DeactiveCompany.Orchestrators;

namespace KOG.ECommerce.Features.Companies.DeactiveCompany;

public record DeactiveCompanyRequestViewModel(string ID);
public class DeactiveCompanyRequestValidator : AbstractValidator<DeactiveCompanyRequestViewModel>
{
    public DeactiveCompanyRequestValidator()
    {
        RuleFor(request => request.ID).NotEmpty().Length(1, 100);
    }
}

public class DeactiveCompanyEndPointRequestProfile : Profile
{
    public DeactiveCompanyEndPointRequestProfile()
    {
        CreateMap<DeactiveCompanyRequestViewModel, DeactiveCompanyOrchestrator>();
    }
}