using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Noty.Structure.Converters
{
    public class FontWeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch(value.ToString())
            {
                case "Normal":
                    return FontWeights.Normal;
                case "Bold":
                    return FontWeights.Bold;
                case "Thin":
                    return FontWeights.Thin;
                default: 
                    return FontWeights.Normal;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value.ToString();
    }
}
