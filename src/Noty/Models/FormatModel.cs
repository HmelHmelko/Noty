using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Noty.Models
{
    public class FormatModel : ObservableModel
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
            get => Properties.Settings.Default.FontSize;
            set
            {
                Properties.Settings.Default.FontSize = value;
                Properties.Settings.Default.Save();
            }
        }
        public FontStyle FontStyle
        {
            get => Properties.Settings.Default.FontStyle;
            set
            {
                Properties.Settings.Default.FontStyle = value;
                Properties.Settings.Default.Save();
            }
        }
        public FontWeight FontWeight
        {
            get => Properties.Settings.Default.FontWeight;
            set
            {
                Properties.Settings.Default.FontWeight = value;
                Properties.Settings.Default.Save();
            }
        }
        public FontFamily FontFamily
        {
            get => Properties.Settings.Default.FontFamily;
            set
            {
                Properties.Settings.Default.FontFamily = value;
                Properties.Settings.Default.Save();
            }
        }
        public TextWrapping TextWrapping { get; set; } = TextWrapping.NoWrap;
        public FormatModel()
        {
            Properties.Settings.Default.SettingsSaving += RefreshPropertiesOnSettingSaving;

        }

        private void RefreshPropertiesOnSettingSaving(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FontSize = Properties.Settings.Default.FontSize;
            FontFamily = Properties.Settings.Default.FontFamily;
            FontStyle = Properties.Settings.Default.FontStyle;
            FontWeight = Properties.Settings.Default.FontWeight;
        }
    }
}
