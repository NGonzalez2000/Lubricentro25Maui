using System.Globalization;

namespace Lubricentro25.Converters;

public class DNICUIL_StringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string str) return "";

        if (str.Length < 9) return str;
        while (str.Length < 11) str = "0" + str;

        return $"{str[..2]}-{str.Substring(2, 8)}-{str[^1]}";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string str) return "";

        return str.TrimStart('0').Replace("-", "");
    }
}
