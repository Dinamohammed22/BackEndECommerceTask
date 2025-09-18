using AutoMapper;
using AutoMapper.QueryableExtensions;
using KOG.ECommerce.Common.Views;
using System.Collections;

namespace KOG.ECommerce.Helpers;

public static class MapperHelper
{
    public static IMapper Mapper { get; set; }

    public static IQueryable<TResult> Map<TResult>(this IQueryable source)
    {
        return source.ProjectTo<TResult>(Mapper.ConfigurationProvider);
    }

    public static IEnumerable<TResult> MapList<TResult>(this IEnumerable source)
    {
        return source.AsQueryable().ProjectTo<TResult>(Mapper.ConfigurationProvider);
    }

    public static PagingViewModel<TResult> MapPage<TSource, TResult>(this PagingViewModel<TSource> source)
    {
        var items = source.Items.AsQueryable().ProjectTo<TResult>(Mapper.ConfigurationProvider);

        return new PagingViewModel<TResult>
        {
            Items = items,
            PageIndex = source.PageIndex,
            PageSize = source.PageSize,
            Pages = source.Pages,
            Records = source.Records,
        };
    }

    public static TResult MapOne<TResult>(this object source)
    {
        return Mapper.Map<TResult>(source);
    }
}