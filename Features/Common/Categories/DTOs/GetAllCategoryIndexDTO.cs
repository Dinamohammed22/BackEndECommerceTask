using AutoMapper;
using KOG.ECommerce.Models.Categories;

namespace KOG.ECommerce.Features.Common.Categories.DTOs
{
    //public record GetAllCategoryIndexDTO(string ID,
    //    string Name,
    //    string? ParentCategoryId,
    //    int ProductCount,
    //    int SubcategoryCount,
    //    bool IsActive,
    //    string ImagePath);
    public class GetAllCategoryIndexDTO
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string? ParentCategoryId { get; set; }
        public int ProductCount { get; set; }
        public int SubCategoryCount { get; set; }
        public bool IsActive { get; set; }
        public string ImagePath { get; set; }
    }

    public class GetAllCategoryIndexDTOProfile : Profile
    {
        public GetAllCategoryIndexDTOProfile()
        {
            CreateMap<Category, GetAllCategoryIndexDTO>();
        }
    }
}
