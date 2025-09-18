using AutoMapper;
using KOG.ECommerce.Common.Views;

namespace KOG.ECommerce.Features.Classifications.SelectListClassification
{
    public record SelectListClassificationResponseViewModel(string Name, string ID);
    public class SelectListClassificationResponseProfile : Profile
    {
        public SelectListClassificationResponseProfile() {
            CreateMap<SelectListItemViewModel, SelectListClassificationResponseViewModel>();
        }
    }


}
