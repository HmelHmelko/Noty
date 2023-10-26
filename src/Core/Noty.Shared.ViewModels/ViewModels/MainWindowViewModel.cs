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
        private DelegateCommand addTabCommand;
        public DelegateCommand AddTabItemCommand
        {
            get
            {
                return addTabCommand ??
                    (addTabCommand = new DelegateCommand(obj =>
                    {
                        var tab = ((FileTabViewModel)obj);
                        TabFileItems.Add(new FileTabViewModel());
                        CurrentTabFileItem = TabFileItems.LastOrDefault();
                    }));
            }
        }

        private DelegateCommand pinTabCommand;
        public DelegateCommand PinTabFileCommand
        {
            get
            {
                return pinTabCommand ??
                    (pinTabCommand = new DelegateCommand(obj =>
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
                    }));
            }
        }

        private DelegateCommand closeTabCommand;
        public DelegateCommand CloseTabFileCommand
        {
            get
            {
                return closeTabCommand ??
                    (closeTabCommand = new DelegateCommand(obj =>
                    {
                        var tab = ((FileTabViewModel)obj);
                        TabFileItems.Remove(tab);
                        if (tab == CurrentTabFileItem)
                        CurrentTabFileItem = TabFileItems.LastOrDefault();
                    }));
            }
        }

        #endregion

        #region Menu

        private DelegateCommand newCommand;
        public DelegateCommand NewFileCommand
        {
            get
            {
                return newCommand ?? 
                    (newCommand = new DelegateCommand(obj =>
                    {
                        if (dialogService.SaveFileDialog() == true)
                        {
                            CurrentTabFileItem = new FileTabViewModel(string.Empty, new FileInfo(dialogService.FilePath).Name, dialogService.FilePath);                          
                            TabFileItems.Add(CurrentTabFileItem);
                            txtFileService.NewFile(dialogService.FilePath, CurrentTabFileItem.FileName);

                            //dialogService.ShowMessage("Файл открыт");
                        }
                    }));
            }
        }

        private DelegateCommand increaseSizeCommand;
        public DelegateCommand IncreaseFontSizeCommand
        {
            get
            {
                return increaseSizeCommand ??
                    (increaseSizeCommand = new DelegateCommand(obj =>
                    {
                        CurrentTabFileItem.TextFontSize += 1.0;
                    }));
            }
        }

        private DelegateCommand openCommand;
        public DelegateCommand OpenFileCommand
        {
            get
            {
                return openCommand ??
                    (openCommand = new DelegateCommand(obj =>
                    {
                        if (dialogService.OpenFileDialog() == true)
                        {
                            CurrentTabFileItem = new FileTabViewModel(txtFileService.Open(dialogService.FilePath), 
                                new FileInfo(dialogService.FilePath).Name, dialogService.FilePath);
                            TabFileItems.Add(CurrentTabFileItem);
                            //dialogService.ShowMessage("Файл открыт");
                        }
                    }));
            }
        }

        private DelegateCommand saveCommand;
        public DelegateCommand SaveFileCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand = new DelegateCommand(obj =>
                    {
                        if (dialogService.FileExtansion == ".txt")
                            txtFileService.Save(dialogService.FilePath, CurrentTabFileItem.TextContent);
                        else
                            rtfFileService.Save(dialogService.FilePath, CurrentTabFileItem.TextContent);                 
                        
                        //dialogService.ShowMessage("Файл сохранен");
                    }));
            }
        }

        private DelegateCommand saveAsCommand;
        public DelegateCommand SaveAsFileCommand
        {
            get
            {
                return saveAsCommand ??
                    (saveAsCommand = new DelegateCommand(obj =>
                    {
                        if (dialogService.SaveFileDialog() == true)
                        {
                            if (dialogService.FileExtansion == ".txt")
                                txtFileService.Save(dialogService.FilePath, CurrentTabFileItem.TextContent);
                            else
                                rtfFileService.Save(dialogService.FilePath, CurrentTabFileItem.TextContent);
                        //dialogService.ShowMessage("Файл сохранен");
                    }
                }));
            }
        }

        #endregion

        #region Application OnCloseLogic

        private DelegateCommand closeCommand;
        public DelegateCommand CloseAppCommand
        {
            get
            {
                return closeCommand ??
                    (closeCommand = new DelegateCommand(obj =>
                    {

                    }));
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
