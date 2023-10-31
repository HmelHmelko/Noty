using System.Windows;
using System.Windows.Media;

namespace Noty.Models
{
    public class FormatModel
    {
        public double FontSize { get; set; } = 60;
        public FontStyle FontStyle { get; set; }
        public FontWeight FontWeight { get; set; }
        public FontFamily FontFamily { get; set; }
        public TextWrapping TextWrapping { get; set; } = TextWrapping.Wrap;

    }
}
