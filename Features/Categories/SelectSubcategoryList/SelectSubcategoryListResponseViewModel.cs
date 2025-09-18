using AutoMapper;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Categories.SelectCategoryList;

namespace KOG.ECommerce.Features.Categories.SelectSubcategoryList
{
    public record SelectSubcategoryListResponseViewModel(string Name, string ID);
    public class SelectSubcategoryListResponseProfile:Profile
    {
        public SelectSubcategoryListResponseProfile()
        {
            CreateMap<SelectListItemViewModel, SelectSubcategoryListResponseViewModel>();
        }
    }
}
