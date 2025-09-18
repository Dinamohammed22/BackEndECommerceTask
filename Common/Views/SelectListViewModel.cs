
using KOG.ECommerce.Common.Interfaces;

namespace KOG.ECommerce.Common.Views;

public class SelectListItemViewModel : ISelectableListItem
{
    public string ID { get; set; }
    public string Name { get; set; }
}
public static class SelectListViewModelExtension
{
    public static SelectListItemViewModel ToSelectListItemViewModel(this ISelectableListItem obj)
    {
        return new SelectListItemViewModel
        {
            ID = obj.ID,
            Name = obj.Name,
        };
    }

    public static IEnumerable<SelectListItemViewModel> ToSelectListViewModel(this IQueryable<ISelectableListItem> list)
    {
        return list.Select(obj => obj.ToSelectListItemViewModel());
    }
}