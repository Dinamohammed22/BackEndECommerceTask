using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Discounts.Queries;

namespace KOG.ECommerce.Features.Discounts.GetDiscountByID
{
    public record GetDiscountByIDRequestViewModel(string ID);
    public class GetDiscountByIDRequestValidator:AbstractValidator<GetDiscountByIDRequestViewModel>
    {
        public GetDiscountByIDRequestValidator() { }
    }
    public class GetDiscountByIDRequestProfile:Profile
    {
        public GetDiscountByIDRequestProfile()
        {
            CreateMap<GetDiscountByIDRequestViewModel, GetDiscountByIDQuery>();
        }
    }
}
