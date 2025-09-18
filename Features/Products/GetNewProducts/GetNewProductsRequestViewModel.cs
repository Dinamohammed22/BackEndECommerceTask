using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Products.Queries;

namespace KOG.ECommerce.Features.Products.GetNewProducts
{
    public record GetNewProductsRequestViewModel(int NumberOfProducts=3);
    public class GetNewProductsRequestValidator : AbstractValidator<GetNewProductsRequestViewModel>
    {
        public GetNewProductsRequestValidator()
        {
        }
    }
    public class GetNewProductsRequestProfile : Profile
    {
        public GetNewProductsRequestProfile()
        {
            CreateMap<GetNewProductsRequestViewModel, GetNewProductsQuery>();
        }
    }
}
