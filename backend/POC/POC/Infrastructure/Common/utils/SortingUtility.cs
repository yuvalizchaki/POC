using System.Reflection;

namespace POC.Infrastructure.Common.utils;

public static class SortingUtility
{
    public static IEnumerable<T> SortItemsByFieldNames<T>(IEnumerable<T> items, List<string> sortingFields)
    {
        IOrderedEnumerable<T> sortedItems = null;

        foreach (var field in sortingFields)
        {
            sortedItems = sortedItems == null 
                ? items.OrderBy(item => GetPropertyValue(item, field)) 
                : sortedItems.ThenBy(item => GetPropertyValue(item, field));
        }

        return sortedItems ?? items;
    }

    private static object GetPropertyValue<T>(T obj, string propertyName)
    {
        PropertyInfo propInfo = typeof(T).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
        return propInfo?.GetValue(obj, null);
    }
}