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

        private DelegateCommand changeStateCommand;
        public DelegateCommand ChangeWindowStateCommand
        {
            get
            {
                return changeStateCommand ??
                    (changeStateCommand = new DelegateCommand(obj =>
                    {
                        ChangeSizeState();
                    }));
            }
        }

        private DelegateCommand minimizeCommand;
        public DelegateCommand MinimizeWindowCommand
        {
            get
            {
                return minimizeCommand ??
                    (minimizeCommand = new DelegateCommand(obj =>
                    {
                        MinimizeToTaskBar();
                    }));
            }
        }

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

        private DelegateCommand closeApplicationCommand;
        public DelegateCommand CloseApplicationCommand
        {
            get
            {
                return closeApplicationCommand ??
                    (closeApplicationCommand = new DelegateCommand(obj =>
                    {
                        mainVM.CloseAppCommand.Execute(obj);
                        CloseWindow();
                    }));
            }
        }

        public void CloseWindow() => Application.Current.Shutdown();
        #endregion
    }
}
