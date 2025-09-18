using AutoMapper;
using KOG.ECommerce.Features.Common.Categories.DTOs;

namespace KOG.ECommerce.Features.Categories.GetCategory
{
    public record GetCategoryResponseViewModel(
    string Id,
    string Name,
    string? ParentCategoryId,
    List<string>? Tags,
    List<CategoryDTO> Subcategories
);

    public class GetCategoryResponseProfile : Profile
    {
        public GetCategoryResponseProfile()
        {
            CreateMap<CategoryDTO, GetCategoryResponseViewModel>()
                .ForMember(dest => dest.Subcategories, opt => opt.MapFrom(src => src.Subcategories));
        }
    }

}
