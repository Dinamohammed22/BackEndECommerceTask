using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Companies.Queries;

namespace KOG.ECommerce.Features.Companies.CompanyFilterByName
{
    public record CompanyFilterByNameRequestViewModel(
        string? NID,
        DateTime? From,
        DateTime? To,
        int pageIndex = 1,
        int pageSize = 100,
        string? CityID = null,
        string? GovernorateID = null,
        string? CompanyName = null
    );
    public class CompanyFilterByNameRequestValidator : AbstractValidator<CompanyFilterByNameRequestViewModel>
    {
        public CompanyFilterByNameRequestValidator() { 
            
        }
    }
    public class CompanyFilterByNameRequestProfile : Profile
    {
        public CompanyFilterByNameRequestProfile() {

            CreateMap<CompanyFilterByNameRequestViewModel, CompanyFilterByNameQuery>();
        }
    }
}
