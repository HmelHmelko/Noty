using Noty.Services;
using Noty.Shared.ViewModels;
using Noty.Shared.FileOperations;
using System.Windows;
using System.Windows.Input;

namespace Noty
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        { 
            InitializeComponent();
            DataContext = new MainWindowViewModel(new DefaultDialogService(), new TxtFileService(textArea));
        }
        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var hm = e.GetPosition(this);
            textArea.Document.FontSize += 1;
        }

    }
}
