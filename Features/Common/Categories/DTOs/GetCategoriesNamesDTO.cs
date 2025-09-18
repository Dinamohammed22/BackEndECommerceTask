using AutoMapper;
using KOG.ECommerce.Models.Categories;

namespace KOG.ECommerce.Features.Common.Categories.DTOs
{
    public record GetCategoriesNamesDTO(string ID,string Name,int ProductsNumber);
    public class GetCategoriesNamesProfileDTO:Profile
    {
        public GetCategoriesNamesProfileDTO()
        {
            CreateMap<Category, GetCategoriesNamesDTO>();
        }
    }


}
