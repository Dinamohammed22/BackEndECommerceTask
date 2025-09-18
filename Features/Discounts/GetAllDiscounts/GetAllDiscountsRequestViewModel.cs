using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Discounts.Queries;

namespace KOG.ECommerce.Features.Discounts.GetAllDiscounts
{
    public record GetAllDiscountsRequestViewModel(int pageIndex = 1, int pageSize = 100);
    public class GetAllDiscountsRequestValidator : AbstractValidator<GetAllDiscountsRequestViewModel>
    {
        public GetAllDiscountsRequestValidator() { }
    }
    public class GetAllDiscountsRequestProfile : Profile
    {
        public GetAllDiscountsRequestProfile() {
            CreateMap<GetAllDiscountsRequestViewModel, GetAllDiscountsQuery>();
        }
    }
}
