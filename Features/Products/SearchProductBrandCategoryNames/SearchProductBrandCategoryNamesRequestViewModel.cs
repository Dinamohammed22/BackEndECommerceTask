using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Products.Queries;

namespace KOG.ECommerce.Features.Products.SearchProductBrandCategoryNames
{
    public record SearchProductBrandCategoryNamesRequestViewModel(string Name);
    public class SearchProductBrandCategoryNamesRequestValidator : AbstractValidator<SearchProductBrandCategoryNamesRequestViewModel>
    {
        public SearchProductBrandCategoryNamesRequestValidator()
        {
        }
    }
    public class SearchProductBrandCategoryNamesRequestProfile : Profile
    {
        public SearchProductBrandCategoryNamesRequestProfile()
        {
            CreateMap<SearchProductBrandCategoryNamesRequestViewModel, SearchProductBrandCategoryNamesQuery>();
        }
    }
}
