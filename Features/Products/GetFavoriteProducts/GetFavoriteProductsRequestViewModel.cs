using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Products.Queries;

namespace KOG.ECommerce.Features.Products.GetFavoriteProducts
{
    public record GetFavoriteProductsRequestViewModel(int NumberOfProducts=3);

    public class GetFavoriteProductsRequestValidator : AbstractValidator<GetFavoriteProductsRequestViewModel>
    {
        public GetFavoriteProductsRequestValidator() { }
    }
    public class GetFavoriteProductsRequestProfile : Profile
    {
        public GetFavoriteProductsRequestProfile()
        {
            CreateMap<GetFavoriteProductsRequestViewModel, GetFavoriteProductsQuery>();

        }

    }
}
