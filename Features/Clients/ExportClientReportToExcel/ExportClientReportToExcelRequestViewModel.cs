using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Clients.Queries;

namespace KOG.ECommerce.Features.Clients.ExportClientReportToExcel
{
    public record ExportClientReportToExcelRequestViewModel(string? Name, DateTime? From, DateTime? To);
    public class ExportClientReportToExcelRequestValidator : AbstractValidator<ExportClientReportToExcelRequestViewModel>
    {
        public ExportClientReportToExcelRequestValidator()
        {
        }
    }
    public class ExportClientReportToExcelRequestProfile : Profile
    {
        public ExportClientReportToExcelRequestProfile()
        {
            CreateMap<ExportClientReportToExcelRequestViewModel, ExportClientReportToExcelQuery>();
        }
    }
}
