using AutoMapper;
using KOG.ECommerce.Common.Views;

namespace KOG.ECommerce.Features.ClientGroups.SelectClientGroupList
{
    public record SelectClientGroupListResponseViewModel(string Name, string ID);

    public class SelectClientGroupListResponseProfile : Profile
    {
        public SelectClientGroupListResponseProfile()
        {
            CreateMap<SelectListItemViewModel, SelectClientGroupListResponseViewModel>();
        }
    }
}
