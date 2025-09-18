using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Orders.Queries;
using KOG.ECommerce.Features.Orders.GetOrderByID.Orchestrators;

namespace KOG.ECommerce.Features.Orders.GetOrderByID
{
    public record GetOrderByIDRequestViewModel(string ID);
    public class GetOrderByIDRequestValidator:AbstractValidator<GetOrderByIDRequestViewModel>
    {
        public GetOrderByIDRequestValidator() { }
    }
    public class GetOrderByIDRequestProfile : Profile
    {
        public GetOrderByIDRequestProfile()
        {
            CreateMap<GetOrderByIDRequestViewModel, GetOrderByIDOrchestrator>();
        }
    }
}
