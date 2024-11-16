using System.Globalization;

namespace Lubricentro25.Converters;

public class StringToNumberConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if(value is int i) return i.ToString();
        return string.Empty;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string s || s.Length == 0) return 0;

        string ret = s.StartsWith('-') ? "-" : string.Empty;
        

        for (int i = 0; i < s.Length; i++)
        {
            if (char.IsDigit(s[i]))
            {
                ret+=s[i];
            }
        }
        bool ok = int.TryParse(ret, out int rta);

        return ok ? rta : 0;
    }
}
