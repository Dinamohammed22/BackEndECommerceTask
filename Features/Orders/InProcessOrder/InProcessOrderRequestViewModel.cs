using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Orders.InProcessOrder.Commands;

namespace KOG.ECommerce.Features.Orders.InProcessOrder
{
    public record InProcessOrderRequestViewModel(string ID);
    public class InProcessOrderRequestValidator : AbstractValidator<InProcessOrderRequestViewModel>
    {
        public InProcessOrderRequestValidator() { }
    }
    public class InProcessOrderRequestProfile : Profile
    {
        public InProcessOrderRequestProfile() {
            CreateMap<InProcessOrderRequestViewModel, InProcessOrderCommand>();
        }
    }
}
