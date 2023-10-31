using Microsoft.Win32;
using Noty.Commands;
using Noty.Models;
using Noty.Services;
using Noty.Shared.FileOperations;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace Noty.ViewModels
{
    public class FileTabViewModel : BaseViewModel
    {
        #region private Fields
        private IFileServiceCreator FileServiceCreator;
        #endregion

        #region Properties
        public DocumentModel Document { get; private set; }
        public ObservableCollection<TabViewModel> TabItems { get; set; } =
            new ObservableCollection<TabViewModel>();
        public TabViewModel CurrentTab { get; set; }
        public string CurrentMethod { get; set; } = "TEST";
        #endregion

        #region Commands

        #region Tabs
        public ICommand AddTabItemCommand { get; }
        private bool CanAddTabItemCommandExecute(object parameter) => true;
        private void OnAddTabItemCommandExecuted(object parameter)
        {
            var filepath = Directory.GetParent(Directory.GetCurrentDirectory()).ToString() + "\\";
            var defaultFileName = "Untitled.txt";
            var untitledCount = 0;

            while (File.Exists(filepath + defaultFileName))
            {
                untitledCount++;
                defaultFileName = $"Untitled ({untitledCount}).txt";
            }

            Document = new DocumentModel();
            Document.FileName = defaultFileName;
            Document.FilePath = filepath + defaultFileName;
            Document.TextContent = string.Empty;

            FileServiceCreator.CreateService(Document.FilePath).NewFile();
            TabItems.Add(CurrentTab = new TabViewModel(Document));
        }

        #endregion

        #region TopMenu (main)
        public ICommand SaveFileCommand { get; }
        private bool CanSaveFileCommandExecute(object parameter) => CurrentTab != null;
        private void OnSaveFileCommandExecuted(object parameter) => 
            FileServiceCreator.CreateService(Document.FilePath).Save(Document.TextContent);

        public ICommand SaveAsFileCommand { get; }
        private bool CanSaveAsFileCommandExecute(object parameter) => CurrentTab != null;
        private void OnSaveAsFileCommandExecuted(object parameter)
        {
            SaveFileCommand.Execute(Document.FilePath);
            var dialog = new DefaultDialogService();
            if (dialog.SaveFileDialog())
            {
                FileServiceCreator.CreateService(dialog.FilePath).SaveAs(Document.TextContent, Path.GetExtension(dialog.FilePath));
            }
        }

        public ICommand NewFileCommand { get; }
        private bool CanNewFileCommandExecute(object parameter) => true;
        private void OnNewFileCommandExecuted(object parameter)
        {
            var dialog = new DefaultDialogService();
            if (dialog.SaveFileDialog())
            {
                Document = new DocumentModel();
                Document.FilePath = dialog.FilePath;
                Document.FileName = Path.GetFileName(dialog.FilePath);
                Document.TextContent = string.Empty;

                TabItems.Add(new TabViewModel(Document));

                FileServiceCreator.CreateService(dialog.FilePath).NewFile();
            }
        }

        public ICommand OpenFileCommand { get; }
        private bool CanOpenFileCommandExecute(object parameter) => true;
        private void OnOpenFileCommandExecuted(object parameter)
        {
            var dialog = new DefaultDialogService();
            if (dialog.OpenFileDialog())
            {
                Document = new DocumentModel();
                Document.FilePath = dialog.FilePath;
                Document.FileName = Path.GetFileName(dialog.FilePath);
                Document.TextContent = FileServiceCreator.CreateService(Document.FilePath).Open();

                TabItems.Add(CurrentTab = new TabViewModel(Document));
            }
        }
        #endregion

        #endregion

        #region Constructors
        public FileTabViewModel()
        {
            FileServiceCreator = new FileServiceCreator<TxtFileService>();

            #region Commands init
            SaveFileCommand = new DelegateCommand(OnSaveFileCommandExecuted, CanSaveFileCommandExecute);
            SaveAsFileCommand = new DelegateCommand(OnSaveAsFileCommandExecuted, CanSaveAsFileCommandExecute);
            NewFileCommand = new DelegateCommand(OnNewFileCommandExecuted, CanNewFileCommandExecute);
            OpenFileCommand = new DelegateCommand(OnOpenFileCommandExecuted, CanOpenFileCommandExecute);
            AddTabItemCommand = new DelegateCommand(OnAddTabItemCommandExecuted, CanAddTabItemCommandExecute);
            #endregion
        }
        #endregion
    }
}
