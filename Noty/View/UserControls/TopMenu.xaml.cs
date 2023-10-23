using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Noty;

namespace Noty.View.UserControls
{
    public partial class TopMenu : UserControl
    {
        public MainMenuItems MainMenu;
        private RichTextBox textCont;
        public TopMenu()
        {
            InitializeComponent();
            MainMenu = new MainMenuItems();

            var window = Window.GetWindow(this) as MainWindow;
            if (window != null)
                textCont = window.textArea;
        }

        private void OpenMenuItem_Click(object sender, RoutedEventArgs e) => MainMenu.OpenFile(textCont);
        private void NewMenuItem_Click(object sender, RoutedEventArgs e) => MainMenu.NewFile(textCont);
        private void SaveMenuItem_Click(object sender, RoutedEventArgs e) => MainMenu.SaveFile(textCont);
    }
}
