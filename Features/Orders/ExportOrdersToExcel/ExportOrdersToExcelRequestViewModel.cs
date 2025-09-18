using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Orders.Queries;
using KOG.ECommerce.Features.Orders.GetAllOrders.Orchestrator;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Orders.ExportOrdersToExcel
{
    public record ExportOrdersToExcelRequestViewModel(string? CustomerName, string? CustomerNumber, string? OrderNumber,
        OrderStatus? OrderStatus, double? TotalPrice, DateTime? From, DateTime? To);
    public class ExportOrdersToExcelRequestValidaator : AbstractValidator<ExportOrdersToExcelRequestViewModel>
    {
        public ExportOrdersToExcelRequestValidaator()
        {
        }
    }
    public class ExportOrdersToExcelRequestProfile : Profile
    {
        public ExportOrdersToExcelRequestProfile() {
            CreateMap<ExportOrdersToExcelRequestViewModel, ExportOrdersToExcelQuery>();
            CreateMap<ExportOrdersToExcelQuery, GetAllOrdersOrchestrator>();
        }
    }
}
