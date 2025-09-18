using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Orders.CompletedOrder.Commands;
using KOG.ECommerce.Features.Orders.CompletedOrder.Orchestrators;

namespace KOG.ECommerce.Features.Orders.CompletedOrder
{
    public record CompletedOrderRequestViewModel(string ID);
    public class CompletedOrderRequestValidator : AbstractValidator<CompletedOrderRequestViewModel>
    {
        public CompletedOrderRequestValidator() { }
    }
    public class CompletedOrderRequestProfile : Profile
    {
        public CompletedOrderRequestProfile()
        {
            CreateMap<CompletedOrderRequestViewModel, CompleteOrderOrchestrator>();
        }
    }
}
