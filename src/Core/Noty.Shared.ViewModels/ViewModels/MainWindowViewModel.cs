using System.Collections.ObjectModel;
using System.Windows.Input;
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

        #region Commands

        #region MouseDragTop
        public ICommand MouseDragCommand { get; }
        #endregion

        #region Menu
        public ICommand NewFileCommand { get; }
        public ICommand OpenFileCommand { get; }
        public ICommand SaveFileCommand { get; }
        public ICommand SaveAsFileCommand { get; }
        public ICommand IncreaseFontSizeCommand { get; }
        public ICommand CloseAppCommand { get; }
        public ICommand MinimizeWindowCommand { get; }
        public ICommand ChangeWindowStateCommand { get; }
        #endregion

        #region Tabs
        public ICommand CloseTabFileCommand { get; }
        public ICommand PinTabFileCommand { get; }
        #endregion

        #endregion

        #region DelegateCommands

        private DelegateCommand pinTabCommand;
        public DelegateCommand PinTabCommand
        {
            get
            {
                return pinTabCommand ??
                  (pinTabCommand = new DelegateCommand(obj =>
                  {
                      var tab = (FileTabViewModel)obj;
                      var indexOfTab = TabFileItems.IndexOf(tab);

                      if(!tab.IsPinned)
                          tab.IsPinned = true;
                      else
                        tab.IsPinned = false;

                      if (!tab.IsPinned)
                      {
                          TabFileItems.Insert(FirstNonPinnedTab, tab);
                          TabFileItems.RemoveAt(indexOfTab);
                          
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
        public DelegateCommand CloseTabCommand
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

        private DelegateCommand changeStateCommand;
        public DelegateCommand ChangeStateCommand
        {
            get
            {
                return changeStateCommand ??
                  (changeStateCommand = new DelegateCommand(obj =>
                  {
                      var myWindow = (IMinimizable)obj;
                      myWindow.ChangeSizeState();
                  }));
            }
        }

        private DelegateCommand minimizeCommand;
        public DelegateCommand MinimizeCommand
        {
            get
            {
                return minimizeCommand ??
                  (minimizeCommand = new DelegateCommand(obj =>
                  {
                      var myWindow = (IMinimizable)obj;
                      myWindow.MinimizeToTaskBar();
                  }));
            }
        }

        private DelegateCommand closeCommand;
        public DelegateCommand CloseCommand
        {
            get
            {
                return closeCommand ??
                  (closeCommand = new DelegateCommand(obj =>
                  {
                      var myWindow = (IClosable)obj;
                      myWindow.Close();
                  }));
            }
        }

        private DelegateCommand newCommand;
        public DelegateCommand NewCommand
        {
            get
            {
                return newCommand ??
                  (newCommand = new DelegateCommand(obj =>
                  {
                        TabFileItems.Add(new FileTabViewModel(this, string.Empty));
                        CurrentTabFileItem = TabFileItems.FirstOrDefault();
                        SaveCommand.Execute(this);
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
                    CurrentTabFileItem.TextFontSize += 1.0;

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
                    if (dialogService.OpenFileDialog() == true)
                    {
                        CurrentTabFileItem = new FileTabViewModel(this, txtFileService.Open(dialogService.FilePath));
                        TabFileItems.Add(CurrentTabFileItem);

                        CurrentTabFileItem.FilePath = dialogService.FilePath;
                        CurrentTabFileItem.FileName = new FileInfo(dialogService.FilePath).Name;

                        //dialogService.ShowMessage("Файл открыт");
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
                    if (dialogService.SaveFileDialog() == true)
                    {
                          if (dialogService.FileExtansion == ".txt")
                          {
                              txtFileService.Save(dialogService.FilePath, CurrentTabFileItem.TextContent);
                              CurrentTabFileItem.FileName = new FileInfo(dialogService.FilePath).Name;
                          }
                          else
                              rtfFileService.Save(dialogService.FilePath, CurrentTabFileItem.TextContent);
                              
                        //dialogService.ShowMessage("Файл сохранен");
                    }
                  }));
            }
        }

        private DelegateCommand saveAsCommand;
        public DelegateCommand SaveAsCommand
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

        #region Constructor
        public MainWindowViewModel(IDialogService dialogService, IFileService txtfileService, IFileService rtfFileService)
        {
            this.dialogService = dialogService;
            this.txtFileService = txtfileService;
            this.rtfFileService = rtfFileService;

            #region TopMenu Commands

            NewFileCommand = NewCommand;
            OpenFileCommand = OpenCommand;
            SaveFileCommand = SaveCommand;
            SaveAsFileCommand = SaveAsCommand;
            IncreaseFontSizeCommand = IncreaseSizeCommand;

            #endregion

            #region Tab Commands

            CloseTabFileCommand = CloseTabCommand;
            PinTabFileCommand = PinTabCommand;

            #endregion

            #region MainWindow Commands

            CloseAppCommand = CloseCommand;
            MinimizeWindowCommand = MinimizeCommand;
            ChangeWindowStateCommand = ChangeStateCommand;

            #endregion
        }

        #endregion
    }
}
