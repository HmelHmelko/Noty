using System.Windows.Input;
using Noty.Shared.FileOperations;

namespace Noty.Shared.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region private Properties
        private IFileService fileService;
        private IDialogService dialogService;

        #endregion

        #region Properties
        public string TextContent { get; set; }
        public double TextFontSize { get; set; } = 14;
        #endregion

        #region Commands
        public ICommand NewFileCommand { get; }
        public ICommand OpenFileCommand { get; }
        public ICommand SaveFileCommand { get; }
        public ICommand IncreaseFontSizeCommand { get; }

        #endregion

        #region DelegateCommands

        private DelegateCommand newCommand;
        public DelegateCommand NewCommand
        {
            get
            {
                return newCommand ??
                  (newCommand = new DelegateCommand(obj =>
                  {
                      try
                      {
                          if(TextContent != string.Empty)
                          {
                              SaveCommand.Execute(this);
                              TextContent = string.Empty;
                          }
                          else
                              TextContent = string.Empty;
                      }
                      catch (Exception ex)
                      {
                          //dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }

        private DelegateCommand increaseSizeCommand;
        public DelegateCommand IncreaseSizeCommand
        {
            get
            {
                return increaseSizeCommand ??
                  (increaseSizeCommand = new DelegateCommand(obj =>
                  {
                      try
                      {
                          TextFontSize += 1.0;
                      }
                      catch (Exception ex)
                      {
                          //dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }



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

        #endregion

        #region Constructor
        public MainWindowViewModel(IDialogService dialogService, IFileService fileService)
        {
            this.dialogService = dialogService;
            this.fileService = fileService;

            NewFileCommand = NewCommand;
            OpenFileCommand = OpenCommand;
            SaveFileCommand = SaveCommand;
            IncreaseFontSizeCommand = IncreaseSizeCommand;
        }

        #endregion

        public class TextContentViewModel : BaseViewModel
        {
            public string TextContent { get; set; }
            public TextContentViewModel(string textContent)
            {
                TextContent = textContent;
            }
        }
    }
}
