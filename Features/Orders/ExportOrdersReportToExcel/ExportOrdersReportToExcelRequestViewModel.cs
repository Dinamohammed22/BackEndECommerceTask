using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Orders.Queries;
using KOG.ECommerce.Features.Orders.OrdersReports.Orchestrator;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Orders.ExportOrdersToExcel
{
    public record ExportOrdersReportToExcelRequestViewModel(string? CustomerName, string? CustomerNumber, string? OrderNumber,
        OrderStatus? OrderStatus, double? TotalPrice, DateTime? From, DateTime? To);
    public class ExportOrdersToExcelRequestValidator : AbstractValidator<ExportOrdersReportToExcelRequestViewModel>
    {
        public ExportOrdersToExcelRequestValidator()
        {
        }
    }
    public class ExportOrdersReportToExcelRequestProfile : Profile
    {
        public ExportOrdersReportToExcelRequestProfile() {
            CreateMap<ExportOrdersReportToExcelRequestViewModel, ExportOrdersReportToExcelQuery>();
            CreateMap<ExportOrdersReportToExcelQuery, OrdersReportsOrchestrator>();
        }
    }
}
