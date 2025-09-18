using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Companies.Commands;

namespace KOG.ECommerce.Features.CompanyGovernorates.UpdateCompanyGovernorates
{
    public record UpdateCompanyGovernoratesRequestViewModel(string CompanyId, List<string> GovernorateIds);
    public class UpdateCompanyGovernoratesRequestValidator : AbstractValidator<UpdateCompanyGovernoratesRequestViewModel>
    {
        public UpdateCompanyGovernoratesRequestValidator()
        {
        }
    }
    public class UpdateCompanyGovernoratesRequestProfile : Profile
    {
        public UpdateCompanyGovernoratesRequestProfile()
        {
            CreateMap<UpdateCompanyGovernoratesRequestViewModel, UpdateCompanyGovernoratesCommand>();
        }
    }
}
