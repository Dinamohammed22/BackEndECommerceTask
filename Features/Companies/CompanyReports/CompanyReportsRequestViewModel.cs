using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Companies.Queries;

namespace KOG.ECommerce.Features.Companies.CompanyReports
{
    public record CompanyReportsRequestViewModel(
        string? NID,
        DateTime? From,
        DateTime? To,
        int pageIndex = 1,
        int pageSize = 100,
        string? CityID = null,
        string? GovernorateID = null,
        string? CompanyName = null
    );
    public class CompanyReportsRequestValidator : AbstractValidator<CompanyReportsRequestViewModel>
    {
        public CompanyReportsRequestValidator() { 
            
        }
    }
    public class CompanyReportsRequestProfile : Profile
    {
        public CompanyReportsRequestProfile() {

            CreateMap<CompanyReportsRequestViewModel, CompanyReportsQuery>();
        }
    }
}
