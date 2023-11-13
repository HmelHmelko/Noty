using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Noty.Structure.Converters
{
    public class FontFamilyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => new FontFamily(value.ToString());
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value.ToString();
    }
}
