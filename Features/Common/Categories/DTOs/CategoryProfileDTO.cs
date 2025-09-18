using AutoMapper;
using KOG.ECommerce.Models.Categories;
using System.Collections.Generic;

namespace KOG.ECommerce.Features.Common.Categories.DTOs
{
    public record CategoryDTO(string Id, string Name, string? ParentCategoryId, List<string>? Tags, List<CategoryDTO> Subcategories);


    public class CategoryProfileDTO : Profile
    {
        public CategoryProfileDTO()
        {
            CreateMap<Category, CategoryDTO>()
            .ForMember(dest => dest.Subcategories, opt => opt.MapFrom(src => new List<CategoryDTO>())); 

        }
    }

}
