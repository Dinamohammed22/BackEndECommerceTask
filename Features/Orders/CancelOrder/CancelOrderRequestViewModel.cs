using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Orders.CancelOrder.Commands;

namespace KOG.ECommerce.Features.Orders.CancelOrder
{
    public record CancelOrderRequestViewModel(string ID);
    public class CancelOrderRequestValidator : AbstractValidator<CancelOrderRequestViewModel>
    {
        public CancelOrderRequestValidator()
        {

        }
    }
    public class CancelOrderRequestProfile : Profile
    {
        public CancelOrderRequestProfile()
        {
            CreateMap<CancelOrderRequestViewModel, CancelOrderCommand>();
        }
    }
}
