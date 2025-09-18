using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Orders.Queries;
using KOG.ECommerce.Features.Orders.GetAllOrders.Orchestrator;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Orders.GetAllOrders
{
    public record GetAllOrdersRequestViewModel(
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
    public class GetAllOrdersRequestValidator : AbstractValidator<GetAllOrdersRequestViewModel>
    {
        public GetAllOrdersRequestValidator() { }
    }
    public class GetAllOrdersRequestProfile : Profile
    {
        public GetAllOrdersRequestProfile() {
            CreateMap<GetAllOrdersRequestViewModel, GetAllOrdersOrchestrator>();
            CreateMap<GetAllOrdersOrchestrator, GetAllOrdersQuery>();
            CreateMap<GetAllOrdersOrchestrator, GetAllCompanyOrdersQuery>();
        }
    }
}
