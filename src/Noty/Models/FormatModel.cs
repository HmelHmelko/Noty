using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Noty.AppSettings;

namespace Noty.Models
{
    public class FormatModel
    {
        public static Dictionary<string, FontStyle> FontStylesMap = new Dictionary<string, FontStyle>
        {
            { "Normal", FontStyles.Normal },
            { "Italic", FontStyles.Italic },
            { "Oblique", FontStyles.Oblique }
        };

        public static Dictionary<string, FontWeight> FontWeightsMap = new Dictionary<string, FontWeight>
        {
            { "Bold", FontWeights.Bold },
            { "Thin", FontWeights.Thin },
            { "Normal", FontWeights.Normal},
        };
        public double FontSize
        {
            get => Settings.Default.FontSize;
            set { Settings.Default.FontSize = value; }
        }
        public FontStyle FontStyle
        {
            get => Settings.Default.FontStyle;
            set { Settings.Default.FontStyle = value; }
        }
        public FontWeight FontWeight
        {
            get => Settings.Default.FontWeight;
            set { Settings.Default.FontWeight = value; }
        }
        public FontFamily FontFamily
        {
            get => Settings.Default.FontFamily;
            set { Settings.Default.FontFamily = value; }
        }
        public TextWrapping TextWrapping { get; set; } = TextWrapping.NoWrap;
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
