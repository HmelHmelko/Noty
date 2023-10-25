using Noty.Services;
using Noty.Shared.ViewModels;
using System.Windows;

namespace Noty
{
    public partial class MainWindow : Window, IClosable, IMinimizable
    {
        private readonly MainWindowViewModel mainVM;
        
        #region Constructor
        public MainWindow()
        { 
            InitializeComponent();

            mainVM = new MainWindowViewModel(new DefaultDialogService(), new TxtFileService(), new RtfFileService());
            DataContext = mainVM;
        }
        #endregion

        #region Window States/Position Changing
        void IMinimizable.MinimizeToTaskBar()
        {
            this.ShowInTaskbar = true;
            this.WindowState = WindowState.Minimized;
        }

        void IMinimizable.ChangeSizeState() => this.WindowState = 
            this.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;

        private void mainBorder_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => DragMove();
        #endregion

        #region App close
        void IClosable.Close() => Application.Current.Shutdown();
        #endregion
    }
}
