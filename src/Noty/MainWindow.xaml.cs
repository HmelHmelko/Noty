using Noty.Services;
using Noty.Shared.FileOperations;
using Noty.Shared.ViewModels;
using System.Windows;

namespace Noty
{
    public partial class MainWindow : Window, IOnApplicationAction, IMinimizable
    {
        #region readonly
        private readonly MainWindowViewModel mainVM;

        #endregion

        #region Constructor
        public MainWindow()
        { 
            InitializeComponent();

            mainVM = new MainWindowViewModel(new DefaultDialogService(), new FileIdentifier<IFileService>());
            DataContext = mainVM;

        }
        #endregion

        #region Window States/Position Changing
        public DelegateCommand ChangeWindowStateCommand => new DelegateCommand(obj => ChangeSizeState());
        public DelegateCommand MinimizeWindowCommand => new DelegateCommand(obj => MinimizeToTaskBar());
        public void MinimizeToTaskBar()
        {
            this.ShowInTaskbar = true;
            this.WindowState = WindowState.Minimized;
        }

        public void ChangeSizeState() => this.WindowState = 
            this.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        private void mainBorder_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => DragMove();
        #endregion

        #region App close
        public DelegateCommand CloseApplicationCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                    {
                        mainVM.AppClosing.Execute(obj);
                        ApplicationClosing();
                    });
            }
        }

        public void ApplicationClosing() => Application.Current.Shutdown();
        #endregion
    }
}
