using Noty.Services;
using Noty.Shared.ViewModels;
using System.Windows;
using Xceed.Wpf.Toolkit;

namespace Noty
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        { 
            InitializeComponent();
            DataContext = new MainWindowViewModel(new DefaultDialogService(), new FileService(), new RtfFileService());

            //TextArea.TextFormatter = new PlainTextFormatter();
            TextArea.TextFormatter = new RtfFormatter();
        }

        private void Exit_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
    }
}
