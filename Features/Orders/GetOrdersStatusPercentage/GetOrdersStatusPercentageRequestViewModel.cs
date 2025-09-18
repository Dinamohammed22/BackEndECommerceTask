using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Orders.Queries;

namespace KOG.ECommerce.Features.Orders.GetOrdersStatusPercentage
{
    public record GetOrdersStatusPercentageRequestViewModel(DateTime? From , DateTime? To);
    public class GetOrdersStatusPercentageRequestValidator:AbstractValidator<GetOrdersStatusPercentageRequestViewModel>
    {
        public GetOrdersStatusPercentageRequestValidator() { }
    }
    public class GetOrdersStatusPercentageRequestProfile:Profile
    {
        public GetOrdersStatusPercentageRequestProfile()
        {
            CreateMap<GetOrdersStatusPercentageRequestViewModel, GetOrdersStatusPercentageQuery>();
        }
    }

}
