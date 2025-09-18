using AutoMapper;
using KOG.ECommerce.Models.Categories;

namespace KOG.ECommerce.Features.Common.Categories.DTOs
{
    public record CategoryFilterDTO(string Id, string Name, string? ParentCategoryId, List<string>? Tags);
    public class CategoryFilterProfileDTO : Profile
    {
        public CategoryFilterProfileDTO()
        {
            CreateMap<Category, CategoryFilterDTO>();

        }
    }
}
