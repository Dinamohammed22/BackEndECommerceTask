using AutoMapper;
using KOG.ECommerce.Features.Categories.GetCategory;
using KOG.ECommerce.Features.Common.Categories.DTOs;

namespace KOG.ECommerce.Features.Categories.FilterCategory
{
    public record FilterCategoryResponseViewModel(string Id,
    string Name,
    string? ParentCategoryId,
    List<string>? Tags
);

    public class GetCategoryResponseProfile : Profile
    {
        public GetCategoryResponseProfile()
        {
            CreateMap<CategoryFilterDTO, FilterCategoryResponseViewModel>();
        }
    }
}
