using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Orders.Queries;

namespace KOG.ECommerce.Features.Orders.GetOrderState
{
    public record GetOrderStateRequestViewModel(string ID);
    public class GetOrderStateRequestValidator : AbstractValidator<GetOrderStateRequestViewModel>
    {
        public GetOrderStateRequestValidator()
        {
        }
    }

    public class GetOrderStateRequestProfile : Profile
    {
        public GetOrderStateRequestProfile()
        {
            CreateMap<GetOrderStateRequestViewModel, GetOrderStateQuery>();
        }
    }
}
