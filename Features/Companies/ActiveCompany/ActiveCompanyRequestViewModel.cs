using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Companies.ActiveCompany.Commands;
using KOG.ECommerce.Features.Companies.ActiveCompany.Orchestrators;


namespace KOG.ECommerce.Features.Companies.ActiveCompany;

public record ActiveCompanyRequestViewModel(string ID);
public class ActiveCompanyRequestValidator : AbstractValidator<ActiveCompanyRequestViewModel>
{
    public ActiveCompanyRequestValidator()
    {
        RuleFor(request => request.ID).NotEmpty().Length(1, 100);
    }
}
public class ActiveCompanyEndPointRequestProfile : Profile
{
    public ActiveCompanyEndPointRequestProfile()
    {
        CreateMap<ActiveCompanyRequestViewModel, ActiveCompanyOrchestrator>();
        CreateMap<ActiveCompanyOrchestrator, ActiveCompanyCommand>();
    }
}

