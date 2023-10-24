using System.Windows.Input;
using Noty.Shared.FileOperations;

namespace Noty.Shared.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private IFileService fileService;
        private IDialogService dialogService;

        #region Properties
        public string TextContent { get; set; }

        #endregion
        #region Commands
        public ICommand OpenFileCommand { get; }
        public ICommand SaveFileCommand { get; }

        #endregion

        private DelegateCommand openCommand;
        public DelegateCommand OpenCommand
        {
            get
            {
                return openCommand ??
                  (openCommand = new DelegateCommand(obj =>
                  {
                      try
                      {
                          if (dialogService.OpenFileDialog() == true)
                          {
                              TextContent = fileService.Open(dialogService.FilePath);
                              //dialogService.ShowMessage("Файл открыт");
                          }
                      }
                      catch (Exception ex)
                      {
                          //dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }

        private DelegateCommand saveCommand;
        public DelegateCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new DelegateCommand(obj =>
                  {
                      try
                      {
                          if (dialogService.SaveFileDialog() == true)
                          {
                              fileService.Save(dialogService.FilePath, TextContent);
                              //dialogService.ShowMessage("Файл сохранен");
                          }
                      }
                      catch (Exception ex)
                      {
                          //dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }
        public MainWindowViewModel(IDialogService dialogService, IFileService fileService)
        {
            this.dialogService = dialogService;
            this.fileService = fileService;

            OpenFileCommand = OpenCommand;
            SaveFileCommand = SaveCommand;
        }
    }
}
