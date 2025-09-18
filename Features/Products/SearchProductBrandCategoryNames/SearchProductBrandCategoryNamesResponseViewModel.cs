using AutoMapper;

namespace KOG.ECommerce.Features.Products.SearchProductBrandCategoryNames
{
    public record SearchProductBrandCategoryNamesResponseViewModel(string Name);

    public class SearchProductBrandCategoryNamesResponseProfile : Profile
    {
        public SearchProductBrandCategoryNamesResponseProfile()
        {
            CreateMap<string, SearchProductBrandCategoryNamesResponseViewModel>()
                .ForCtorParam("Name", opt => opt.MapFrom(src => src));

            CreateMap<IEnumerable<string>, IEnumerable<SearchProductBrandCategoryNamesResponseViewModel>>()
                .ConvertUsing((src, dest) => src.Select(s => new SearchProductBrandCategoryNamesResponseViewModel(s)));
        }
    }
}
