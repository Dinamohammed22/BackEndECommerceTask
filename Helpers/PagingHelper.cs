using KOG.ECommerce.Common.Views;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Helpers;

public static class PagingHelper
{
    public static PagingViewModel<T> ToPages<T>(this IQueryable<T> query, int pageIndex = 1, int pageSize = 100)
    {
        int records = query.Count();
        if (records <= pageSize || pageIndex <= 0) pageIndex = 1;
        int pages = (int)Math.Ceiling((double)records / pageSize);
        int excludedRows = (pageIndex - 1) * pageSize;
        var items = query.Skip(excludedRows).Take(pageSize).ToList();
        return new PagingViewModel<T>() { PageIndex = pageIndex, PageSize = pageSize, Items = items, Records = records, Pages = pages };
    }

    public static async Task<PagingViewModel<T>> ToPagesAsync<T>(this IQueryable<T> query, int pageIndex = 1, int pageSize = 100)
    {
        int records = query.Count();
        if (records <= pageSize || pageIndex <= 0) pageIndex = 1;
        int pages = (int)Math.Ceiling((double)records / pageSize);
        int excludedRows = (pageIndex - 1) * pageSize;
        var items = await query.Skip(excludedRows).Take(pageSize).ToListAsync();
        return new PagingViewModel<T>() { PageIndex = pageIndex, PageSize = pageSize, Items = items, Records = records, Pages = pages };
    }
}

