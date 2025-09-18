using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Orders.Queries;

namespace KOG.ECommerce.Features.Orders.GetAllOrdersByUserId
{
    public record GetAllOrdersByUserIdRequestViewModel(int pageIndex = 1, int pageSize = 100);
    public class GetAllOrdersByUserIdRequestValidator : AbstractValidator<GetAllOrdersByUserIdRequestViewModel>
    {
        public GetAllOrdersByUserIdRequestValidator()
        {
        }
    }
    public class GetAllOrdersByUserIdRequestProfile : Profile
    {
        public GetAllOrdersByUserIdRequestProfile()
        {
            CreateMap<GetAllOrdersByUserIdRequestViewModel, GetAllOrdersByUserIdQuery>();
        }
    }
}
