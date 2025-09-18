using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Orders.Queries;

namespace KOG.ECommerce.Features.Orders.GetOrderDetails
{
    public record GetOrderDetailsRequestViewModel(string OrderID);
    public class GetOrderDetailsRequestValidator:AbstractValidator<GetOrderDetailsRequestViewModel>
    {
        public GetOrderDetailsRequestValidator() { }
    }
    public class GetOrderDetailsRequestProfile:Profile
    {
        public GetOrderDetailsRequestProfile()
        {
            CreateMap<GetOrderDetailsRequestViewModel, GetOrderDetailsQuery>();
        }
    }
}
