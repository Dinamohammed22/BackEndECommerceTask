using AutoMapper;
using AutoMapper.Execution;
using FluentValidation;
using KOG.ECommerce.Features.Common.Companies.Queries;
using KOG.ECommerce.Features.Companies.Commands;

namespace KOG.ECommerce.Features.Companies.CompanyFilterIndex
{
    public record CompanyFilterRequestViewModel(
        int pageIndex = 1,
        int pageSize = 100,
    string? Name = null,
    string? Mobile = null,
    string? Address = null,
    string? GovernorateId = null,
    string? CityId = null,
    string? Activity = null,
    string? TaxCardID = null,
    string? TaxRegistryNumber = null,
    string? NID = null,
    string? ManagerName = null,
    string? ManagerMobile = null,
    string? ClassificationId = null,
    string? Email=null );

    public class CompanyFilterRequestValidator : AbstractValidator<CompanyFilterRequestViewModel>
    {
        public CompanyFilterRequestValidator()
        {
        }
    }
    public class CompanyFilterEndPointRequestProfile : Profile
    {
        public CompanyFilterEndPointRequestProfile()
        {
            CreateMap<CompanyFilterRequestViewModel, FilterCompanyQuery>();
        }
    }

}
