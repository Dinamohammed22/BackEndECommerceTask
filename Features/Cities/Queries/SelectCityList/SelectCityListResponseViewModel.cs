using AutoMapper;
using KOG.ECommerce.Common.Views;

namespace KOG.ECommerce.Features.Cities.Queries.SelectCityList
{
    public record SelectCityListResponseViewModel(string Name, string ID);
    public class SelectCityListResponseProfile:Profile
    {
        public SelectCityListResponseProfile()
        {
            CreateMap<SelectListItemViewModel, SelectCityListResponseViewModel>();
        }
    }

}
