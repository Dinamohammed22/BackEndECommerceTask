using AutoMapper;
using KOG.ECommerce.Models.Categories;

namespace KOG.ECommerce.Features.Common.Categories.DTOs
{
    public record GetCategoryByIdDTO(string Id, string Name, string? ParentCategoryId, List<string>? Tags, List<string>SEO,string Description, bool IsActive);
    public class GetCategoryByIdDTOProfile : Profile
    {
        public GetCategoryByIdDTOProfile()
        {
            CreateMap<Category, GetCategoryByIdDTO>();
        }
    }

}
