using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Orders.Queries;
using KOG.ECommerce.Features.Orders.OrdersReports.Orchestrator;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Orders.OrdersReports
{
    public record OrdersReportsRequestViewModel(
        string? CustomerName,
        string? CustomerNumber,
        string? OrderNumber,
        OrderStatus? OrderStatus,
        double? TotalPrice, 
        DateTime? From,
        DateTime? To,
        int pageIndex = 1, 
        int pageSize = 100
    );
    public class OrdersReportsRequestValidator : AbstractValidator<OrdersReportsRequestViewModel>
    {
        public OrdersReportsRequestValidator() { }
    }
    public class OrdersReportsRequestProfile : Profile
    {
        public OrdersReportsRequestProfile() {
            CreateMap<OrdersReportsRequestViewModel, OrdersReportsOrchestrator>();
            CreateMap<OrdersReportsOrchestrator, OrdersReportsQuery>();
            CreateMap<OrdersReportsOrchestrator, GetAllCompanyOrdersQuery>();
            CreateMap<OrdersReportsOrchestrator, GetAllCompanyOrdersReportsQuery>();
        }
    }
}
