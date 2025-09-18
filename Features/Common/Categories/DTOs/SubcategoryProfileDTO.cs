using AutoMapper;
using KOG.ECommerce.Models.Categories;
using KOG.ECommerce.Models.Medias;

namespace KOG.ECommerce.Features.Common.Categories.DTOs
{
    public record SubcategoryDTO(string Id, string Name)
    {
        public string Path { get; set; }
    }


    public class SubcategoryProfileDTO : Profile
    {
        public SubcategoryProfileDTO()
        {
            CreateMap<Category, SubcategoryDTO>();
         
        }
    }
}
