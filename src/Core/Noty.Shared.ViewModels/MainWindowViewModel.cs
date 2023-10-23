using System.Windows.Input;
using System.Runtime.CompilerServices;

namespace Noty.Shared.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private IDialogService dialogService;
        public string FilePath { get; set; }
        public string TextContent { get; set; }
        public ICommand OpenFileCommand { get; }
        public ICommand SaveFileCommand { get; }

        private RelayCommand openCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                  (openCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (dialogService.OpenFileDialog() == true)
                          {
                              dialogService.ShowMessage("Файл открыт");
                          }
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (dialogService.SaveFileDialog() == true)
                          {
                              dialogService.ShowMessage("Файл сохранен");
                          }
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }
        public MainWindowViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
            OpenFileCommand = OpenCommand;
            SaveFileCommand = SaveCommand;
        }

        private void Open(object parameter)
        {
            dialogService.OpenFileDialog();
        }
    }
}
