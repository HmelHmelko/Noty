using System.Collections.ObjectModel;
using Noty.Shared.FileOperations;

namespace Noty.Shared.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region private Properties
        private IFileService txtFileService;
        private IFileService rtfFileService;
        private IDialogService dialogService;

        private int LastPinnedTab
        {
            get
            {
                var pinned = TabFileItems.Where(x => x.IsPinned).ToList();
                if (pinned.Count > 0)
                    return TabFileItems.IndexOf(pinned.LastOrDefault());
                else
                    return 0;
            }
        }
        private int FirstNonPinnedTab
        {
            get
            {
                var nonPinned = TabFileItems.Where(x => !x.IsPinned).ToList();
                if (nonPinned.Count > 0)
                    return TabFileItems.IndexOf(nonPinned.FirstOrDefault());
                else
                    return 0;
            }
        }

        #endregion

        #region Properties
        public ObservableCollection<FileTabViewModel> TabFileItems { get; set; } = new ObservableCollection<FileTabViewModel>();
        public FileTabViewModel CurrentTabFileItem { get; set; }
        #endregion

        #region DelegateCommands

        #region Tabs
        public DelegateCommand AddTabItemCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                    {
                        lock (this)
                        {
                            NewFileCommand.Execute(obj);
                        }
                    });
            }
        }
        public DelegateCommand PinTabFileCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                    {
                        var tab = (FileTabViewModel)obj;
                        var indexOfTab = TabFileItems.IndexOf(tab);

                        if (!tab.IsPinned)
                        {
                            TabFileItems.Insert(FirstNonPinnedTab, tab);
                            TabFileItems.RemoveAt(indexOfTab + 1);

                            tab.IsPinned = true;                      
                        }
                        else
                        {
                            TabFileItems.Insert(LastPinnedTab + 1, tab);
                            TabFileItems.RemoveAt(indexOfTab);

                            tab.IsPinned = false;
                        }
                    });
            }
        }

        public DelegateCommand CloseTabFileCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                    {
                        var tab = ((FileTabViewModel)obj);
                        TabFileItems.Remove(tab);

                        if (tab == CurrentTabFileItem)
                            CurrentTabFileItem = TabFileItems.LastOrDefault();
                    });
            }
        }

        #endregion

        #region Menu
        public DelegateCommand NewFileCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                    {
                        if (dialogService.SaveFileDialog() == true)
                        {
                            CurrentTabFileItem = new FileTabViewModel(string.Empty, new FileInfo(dialogService.FilePath).Name, dialogService.FilePath);                          
                            TabFileItems.Add(CurrentTabFileItem);
                            txtFileService.NewFile(dialogService.FilePath, CurrentTabFileItem.FileName);
                        }
                    });
            }
        }
        public DelegateCommand IncreaseFontSizeCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                    {
                        CurrentTabFileItem.TextFontSize += 1.0;
                    });
            }
        }
        public DelegateCommand OpenFileCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                    {
                        if (dialogService.OpenFileDialog() == true)
                        {
                            CurrentTabFileItem = new FileTabViewModel(txtFileService.Open(dialogService.FilePath), 
                                new FileInfo(dialogService.FilePath).Name, dialogService.FilePath);
                            TabFileItems.Add(CurrentTabFileItem);
                        }
                    });
            }
        }
        public DelegateCommand SaveFileCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                    {
                        lock(CurrentTabFileItem)
                        {
                            txtFileService.Save(CurrentTabFileItem.FilePath, CurrentTabFileItem.TextContent);
                        }                 
                    });
            }
        }

        public DelegateCommand SaveAsFileCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                    {
                        if (dialogService.SaveFileDialog() == true)
                        {
                            if (dialogService.FileExtansion == ".txt")
                                txtFileService.Save(dialogService.FilePath, CurrentTabFileItem.TextContent);
                            else
                                rtfFileService.Save(dialogService.FilePath, CurrentTabFileItem.TextContent);
                        }
                    });
            }
        }

        #endregion

        #region Application OnCloseLogic
        public DelegateCommand CloseAppCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                    {

                    });
            }
        }

        #endregion

        #endregion

        #region Constructor
        public MainWindowViewModel(IDialogService dialogService, IFileService txtFileService, IFileService rtfFileService)
        {
            this.dialogService = dialogService;
            this.txtFileService = txtFileService;
            this.rtfFileService = rtfFileService;
        }

        #endregion
    }
}
