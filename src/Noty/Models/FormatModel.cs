using Noty.AppSettings;

namespace Noty.Models
{
    public class FormatModel : ObservableModel
    {
        public double FontSize
        {
            get => Settings.Default.FontSize;
            set { Settings.Default.FontSize = value; }
        }
        public string FontStyle
        {
            get => Settings.Default.FontStyle;
            set { Settings.Default.FontStyle = value; }
        }
        public string FontWeight
        {
            get => Settings.Default.FontWeight;
            set { Settings.Default.FontWeight = value; }
        }
        public string FontFamily
        {
            get => Settings.Default.FontFamily;
            set { Settings.Default.FontFamily = value; }
        }

        public FormatModel()
        {
            Settings.Default.SettingsSaving += RefreshPropertiesOnSettingSaving;
        }

        private void RefreshPropertiesOnSettingSaving(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FontSize = Settings.Default.FontSize;
            FontFamily = Settings.Default.FontFamily;
            FontStyle = Settings.Default.FontStyle;
            FontWeight = Settings.Default.FontWeight;
        }
    }
}
