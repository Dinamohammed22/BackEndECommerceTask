using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Orders.Queries;

namespace KOG.ECommerce.Features.Orders.GetOrderHistory
{
    public record GetOrderHistoryRequestViewModel(int pageIndex = 1, int pageSize = 100);
    public class GetOrderHistoryRequestValidator : AbstractValidator<GetOrderHistoryRequestViewModel>
    {
        public GetOrderHistoryRequestValidator()
        {
        }
    }
    public class GetOrderHistoryRequestProfile : Profile
    {
        public GetOrderHistoryRequestProfile()
        {
            CreateMap<GetOrderHistoryRequestViewModel, GetOrderHistoryQuery>();
        }
    }
}
