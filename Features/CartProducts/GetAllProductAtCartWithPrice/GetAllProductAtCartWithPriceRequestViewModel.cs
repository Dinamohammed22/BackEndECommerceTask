using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.CartProducts.Queries;

namespace KOG.ECommerce.Features.CartProducts.GetAllProductAtCartWithPrice
{
    public record GetAllProductAtCartWithPriceRequestViewModel();
    public class GetAllProductAtCartWithPriceRequestValidator : AbstractValidator<GetAllProductAtCartWithPriceRequestViewModel>
    {

    }
    public class GetAllProductAtCartWithPriceRequestProfile : Profile
    {
        public GetAllProductAtCartWithPriceRequestProfile()
        {
            CreateMap<GetAllProductAtCartWithPriceRequestViewModel, GetAllProductAtCartWithPriceQuery>();
        }
    }
}
