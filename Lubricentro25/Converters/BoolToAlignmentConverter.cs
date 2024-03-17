using System.Globalization;

namespace Lubricentro25.Converters
{
    internal class BoolToAlignmentConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            
            if(value is bool isMine)
            {
                return isMine ? LayoutOptions.End : LayoutOptions.Start;
            }
            return LayoutOptions.End;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if(value is LayoutOptions layoutAlignment)
            {
                return layoutAlignment == LayoutOptions.End;
            }
            return true;
        }
    }
}
