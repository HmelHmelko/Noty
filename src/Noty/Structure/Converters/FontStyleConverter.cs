using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Noty.Structure.Converters
{
    public class FontStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch(value.ToString())
            {
                case "Normal":
                    return FontStyles.Normal;
                case "Oblique":
                    return FontStyles.Oblique;
                case "Italic":
                    return FontStyles.Italic;
                default:
                    return FontStyles.Normal;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value.ToString();
    }
}
