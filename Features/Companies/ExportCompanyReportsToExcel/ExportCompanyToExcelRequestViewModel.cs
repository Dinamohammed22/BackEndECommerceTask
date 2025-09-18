using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Companies.Queries;

namespace KOG.ECommerce.Features.Companies.ExportCompanyReportsToExcel
{
    public record ExportCompanyReportsToExcelRequestViewModel(
        string? NID,
        DateTime? From,
        DateTime? To,
        int pageIndex = 1,
        int pageSize = 100,
        string? CityID = null,
        string? GovernorateID = null,
        string? CompanyName = null
    );
    public class ExportCompanyReportsToExcelRequestValidaator : AbstractValidator<ExportCompanyReportsToExcelRequestViewModel>
    {
        public ExportCompanyReportsToExcelRequestValidaator()
        {
        }
    }
    public class ExportCompanyReportsToExcelRequestProfile : Profile
    {
        public ExportCompanyReportsToExcelRequestProfile() {
            CreateMap<ExportCompanyReportsToExcelRequestViewModel, ExportCompanyReportsToExcelQuery>();
            CreateMap<ExportCompanyReportsToExcelQuery, CompanyReportsQuery>();
        }
    }
}
