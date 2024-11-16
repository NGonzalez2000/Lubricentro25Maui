﻿using System.Globalization;

namespace Lubricentro25.Converters;

class StringToDecimalConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is decimal d)
        {
            return d.ToString("N2", CultureInfo.CurrentCulture);
        }
        return 0m.ToString("N2", CultureInfo.CurrentCulture);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string Text) return 0m;

        string temp = Text.Replace(",", "").Replace(".", "").Replace(" ", "");

        while (temp.Length > 0 && temp[0] == '0') temp = temp.Remove(0, 1);
        if (temp.Length == 0) temp = "0";


        if (decimal.TryParse(temp, NumberStyles.Any, CultureInfo.CurrentCulture, out decimal result))
        {
            decimal ret = result / 100m;

            return ret;
        }
        return 0m;
    }
}