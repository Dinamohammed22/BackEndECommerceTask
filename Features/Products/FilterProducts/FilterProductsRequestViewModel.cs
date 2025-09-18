using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Products.Queries;

namespace KOG.ECommerce.Features.Products.FilterProducts
{
    public record FilterProductsRequestViewModel(List<string>? BrandsId,
        List<string>? CategoriesId,
        List<string>? CompaniesId,
        List<string>? ClassificationId,
        string? ProductName,
        double? FromPrice,
        double? ToPrice,
        double? Liter,
        int PageIndex = 1,
        int PageSize = 10);
    public class FilterProductsRequestValidator : AbstractValidator<FilterProductsRequestViewModel>
    {
        public FilterProductsRequestValidator()
        {
        }
    }
    public class FilterProductsRequestProfile : Profile
    {
        public FilterProductsRequestProfile()
        {
            CreateMap<FilterProductsRequestViewModel, FilterProductsQuery>();
        }
    }
}
