using KOG.ECommerce.Common.Views;
using System.Reflection;

namespace KOG.ECommerce.Helpers;

public static class EnumHelper
{
    public static string GetDescription(this object obj)
    {
        return DescriptionAnnotation.GetDescription(obj);
    }
    public static IEnumerable<T> SearchEnumByName<T>(string searchTerm) where T : Enum
    {
        //if (string.IsNullOrWhiteSpace(searchTerm))
        //    return Enumerable.Empty<T>();

        searchTerm = searchTerm.Trim();

        return Enum.GetValues(typeof(T))
            .Cast<T>()
            .Where(e => e.ToString().IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0);
    }

    public static List<SelectListItemViewModel> ToSelectableList<T>(params T[] exludedValues)
    {
        return
        (from item in Enum.GetValues(typeof(T)).Cast<T>().Select(x => x).ToList()
         where !exludedValues.Contains(item)
         select new SelectListItemViewModel
         {
             ID = item.ToString(),
             Name = DescriptionAnnotation.GetDescription(item),
         }).ToList();
    }

    public static List<SelectListItemViewModel> ToSelectableList<T>(this IEnumerable<T> items)
    {
        return
        (from item in items
         select new SelectListItemViewModel
         {
             ID = item.ToString(),
             Name = DescriptionAnnotation.GetDescription(item),
         }).ToList();
    }

    public static bool EqualsAny<T>(this T obj, params T[] values) where T : Enum
    {
        for (int i = 0; i < values.Length; i++)
        {
            if (obj.ToString() == values[i].ToString())
                return true;
        }

        return false;
    }
    
    public static TEnum[] GetEnumValuesWithAttribute<TEnum, TAttribute>()
        where TEnum : Enum
        where TAttribute : Attribute
    {
        return Enum.GetValues(typeof(TEnum))
            .Cast<TEnum>()
            .Where(value => IsEnumValueAttributed<TEnum, TAttribute>(value))
            .ToArray();
    }

    public static bool IsEnumValueAttributed<TEnum, TAttribute>(TEnum value)
        where TEnum : Enum
        where TAttribute : Attribute

    {
        var fieldInfo = typeof(TEnum).GetField(value.ToString());

        if (fieldInfo != null)
        {
            var deprecatedAttribute = fieldInfo.GetCustomAttribute<TAttribute>();
            return deprecatedAttribute != null;
        }

        return false;
    }
}
