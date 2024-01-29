
using System.Reflection;
using System.Xml.Linq;

namespace BO;

internal static class Tools
{
    public static string ToStringProperty<T>(this T obj)
    {
        if (obj == null)
            return string.Empty;

        Type type = typeof(T);
        PropertyInfo[] properties = type.GetProperties();

        string result = properties
            .Select(prop => $"{prop.Name}: {prop.GetValue(obj) ?? "null"}")
            .Aggregate((acc, next) => $"{acc}\n{next}");

        return result;
    }

    //public static string ToString<T>(this IEnumerable<T> enumerable)
    //{
    //    string enumerableString = "";
    //    foreach (var item in enumerable)
    //    {
    //        enumerableString += item != null ? item.ToString() : "null";
    //        enumerableString += " ";
    //    }
    //    return enumerableString;
    //}
}
