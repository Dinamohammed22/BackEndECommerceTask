using AutoMapper;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Categories.DTOs;

namespace KOG.ECommerce.Features.Categories.SelectCategoryList
{
    public record SelectCategoryListResponseViewModel(string Name, string ID);
    public class SelectCategoryListResponseProfile : Profile
    {
        public SelectCategoryListResponseProfile() {
            CreateMap<SelectListItemViewModel, SelectCategoryListResponseViewModel>();
        }
    }
}
