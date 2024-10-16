using System.Globalization;

namespace Lubricentro25.Converters;

public class Phone_StringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string str) return "";

        if (str.Length < 7) return str;

        return $"{str[..^6]}-{str[^6..]}";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string str) return "";

        return str.Replace("-", "");
    }
}
