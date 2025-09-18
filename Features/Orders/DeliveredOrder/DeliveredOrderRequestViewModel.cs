using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Orders.DeliveredOrder.Commands;
using KOG.ECommerce.Features.Orders.DeliveredOrder.Orchestrators;

namespace KOG.ECommerce.Features.Orders.DeliveredOrder
{
    public record DeliveredOrderRequestViewModel(string ID);
    public class DeliveredOrderRequestValidator : AbstractValidator<DeliveredOrderRequestViewModel>
    {
        public DeliveredOrderRequestValidator()
        {
        }
    }
    public class DeliveredOrderRequestProfile : Profile
    {
        public DeliveredOrderRequestProfile()
        {
            CreateMap<DeliveredOrderRequestViewModel, DeliveredOrderOrchestrator>();
        }
    }
}
