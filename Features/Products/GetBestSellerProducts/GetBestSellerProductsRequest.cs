using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Products.Queries;

namespace KOG.ECommerce.Features.Products.GetBestSellerProducts
{
    public record GetBestSellerProductsRequest(int NumberOfProducts = 3);
    public class GetBestSellerProductsValidator : AbstractValidator<GetBestSellerProductsRequest>
    {
        public GetBestSellerProductsValidator()
        {

        }
    }

    public class GetBestSellerProductsRequestProfile : Profile
    {
        public GetBestSellerProductsRequestProfile()
        {
            CreateMap<GetBestSellerProductsRequest, GetBestSellerProductsQuery>();
        }
    }
}
