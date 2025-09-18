using AutoMapper;
using KOG.ECommerce.Features.Common.Categories.DTOs;

namespace KOG.ECommerce.Features.Categories.GetSubcategories
{
    public record GetSubcategoriesResponseViewModel(string Id, string Name)
    {
        public string Path { get; set; }
    }
    public class GetSubcategoriesResponseProfile:Profile
    {
        public GetSubcategoriesResponseProfile()
        {
            CreateMap<SubcategoryDTO, GetSubcategoriesResponseViewModel>();
        }
    }
}
