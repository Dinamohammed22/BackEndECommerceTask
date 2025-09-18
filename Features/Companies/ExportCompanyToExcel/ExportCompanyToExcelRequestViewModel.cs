using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Companies.Queries;

namespace KOG.ECommerce.Features.Companies.ExportCompanyToExcel
{
    public record ExportCompanyToExcelRequestViewModel(
        string? NID,
        DateTime? From,
        DateTime? To,
        int pageIndex = 1,
        int pageSize = 100,
        string? CityID = null,
        string? GovernorateID = null,
        string? CompanyName = null
    );
    public class ExportCompanyToExcelRequestValidaator : AbstractValidator<ExportCompanyToExcelRequestViewModel>
    {
        public ExportCompanyToExcelRequestValidaator()
        {
        }
    }
    public class ExportCompanyToExcelRequestProfile : Profile
    {
        public ExportCompanyToExcelRequestProfile() {
            CreateMap<ExportCompanyToExcelRequestViewModel, ExportCompanyToExcelQuery>();
            CreateMap<ExportCompanyToExcelQuery, CompanyFilterByNameQuery>();
        }
    }
}
