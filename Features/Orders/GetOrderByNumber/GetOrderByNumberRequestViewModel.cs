using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Orders.GetOrderByNumber.Orchestrators;

namespace KOG.ECommerce.Features.Orders.GetOrderByNumber
{
    public record GetOrderByNumberRequestViewModel(string OrderNumber);
    public class GetOrderByNumberRequestValidator:AbstractValidator<GetOrderByNumberRequestViewModel>
    {
        public GetOrderByNumberRequestValidator() { }
    }
    public class GetOrderByNumberRequestProfile : Profile
    {
        public GetOrderByNumberRequestProfile()
        {
            CreateMap<GetOrderByNumberRequestViewModel, GetOrderByNumberOrchestrator>();
        }
    }
}
