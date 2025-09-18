using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Products.Queries;

namespace KOG.ECommerce.Features.Products.GetProductNameAndPrice
{
    public record GetProductNameAndPriceRequestViewModel(string ID);
    public class GetProductNameAndPriceRequestValidator : AbstractValidator<GetProductNameAndPriceRequestViewModel>
    {
        public GetProductNameAndPriceRequestValidator()
        {

        }
    }
    public class GetProductNameAndPriceRequestProfile:Profile
    {
        public GetProductNameAndPriceRequestProfile()
        {
            CreateMap<GetProductNameAndPriceRequestViewModel, GetProductNameAndPriceQuery>();
        }
    }
}
