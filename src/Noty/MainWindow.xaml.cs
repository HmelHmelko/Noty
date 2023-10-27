using Noty.Services;
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

            mainVM = new MainWindowViewModel(new FileServiceCreator<BaseFileService>());
            DataContext = mainVM;
        }
        #endregion

        #region FileDialog Commands
        public DelegateCommand OpenFileDialogCommand => new DelegateCommand(obj =>
        {
            var dialog = new DefaultDialogService();
            if (dialog.OpenFileDialog())
                mainVM.OpenFileCommand.Execute(dialog.FilePath);
        });

        public DelegateCommand SaveFileDialogCommand => new DelegateCommand(obj =>
        {
            var dialog = new DefaultDialogService();
            if (dialog.SaveFileDialog())
                mainVM.NewFileCommand.Execute(dialog.FilePath);
        });
        public DelegateCommand SaveAsNewFileDialogCommand => new DelegateCommand(obj =>
        {
            var dialog = new DefaultDialogService();
            if (dialog.SaveFileDialog())
                mainVM.SaveAsFileCommand.Execute(dialog.FilePath);
        });
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
