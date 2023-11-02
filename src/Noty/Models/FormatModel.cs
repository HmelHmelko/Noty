using Noty.AppSettings;

namespace Noty.Models
{
    public class FormatModel
    {
        public double FontSize
        {
            get => Settings.Default.FontSize;
            set => Settings.Default.FontSize = value;
        }
        public string FontStyle
        {
            get => Settings.Default.FontStyle;
            set => Settings.Default.FontStyle = value;
        }
        public string FontWeight
        {
            get => Settings.Default.FontWeight;
            set => Settings.Default.FontWeight = value;
        }
        public string FontFamily
        {
            get => Settings.Default.FontFamily;
            set => Settings.Default.FontFamily = value;
        }
    }
}
