using System;
using System.Windows;

namespace Noty.Views.Theme
{
    public class AppResourceThemeDictionary : ResourceDictionary
    {
        private Uri darkGray;
        private Uri darkBlue;

        public Uri DarkGraySource
        {
            get { return darkGray; }
            set
            {
                darkGray = value;
                UpdateSource();
            }
        }
        public Uri DarkBlueSource
        {
            get { return darkBlue; }
            set
            {
                darkBlue = value;
                UpdateSource();
            }
        }

        private void UpdateSource()
        {
            var val = App.Skin == Skin.Red ? DarkGraySource : DarkBlueSource;
            if (val != null && base.Source != val)
                base.Source = val;
        }
    }
}
