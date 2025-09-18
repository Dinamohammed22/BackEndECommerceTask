using AutoMapper;
using KOG.ECommerce.Common.Views;

namespace KOG.ECommerce.Features.Brands.SelectBrandList
{
    public record SelectBrandListResponseViewModel(string Name, string ID);
    public class SelectBrandListResponseProfile : Profile
    {
        public SelectBrandListResponseProfile() {
            CreateMap<SelectListItemViewModel, SelectBrandListResponseViewModel>();
        }
    }
}
