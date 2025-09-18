
namespace KOG.ECommerce.Common.Views;

public class PagingViewModel<T>
{
    public int PageSize { set; get; }
    public int PageIndex { set; get; }
    public int Records { set; get; }
    public int Pages { set; get; }
    public IEnumerable<T> Items { set; get; }

    internal PagingViewModel<T1> MapPage<T1>()
    {
        throw new NotImplementedException();
    }
}
