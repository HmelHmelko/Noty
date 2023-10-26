using System.Collections.ObjectModel;
using Noty.Shared.FileOperations;

namespace Noty.Shared.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region private Properties
        private IFileIdentifier FileIdentifier;
        private IDialogService DialogService;
        private IFileService FileService { get { return FileIdentifier.IdentifyFileExtension(DialogService); } }
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
        public ObservableCollection<FileTabViewModel> TabFileItems { get; set; } =
            new ObservableCollection<FileTabViewModel>();
        public FileTabViewModel CurrentTabFileItem { get; set; }
        #endregion

        #region DelegateCommands
        #region Tabs
        public DelegateCommand AddTabItemCommand => 
            new DelegateCommand(obj => NewFileCommand.Execute(obj));
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
        public DelegateCommand NewFileCommand => new DelegateCommand(obj =>
            {
                if (DialogService.SaveFileDialog() == true)
                {
                    CurrentTabFileItem = new FileTabViewModel(string.Empty, Path.GetFileName(DialogService.FilePath), DialogService.FilePath);                          
                    TabFileItems.Add(CurrentTabFileItem);

                    FileService.NewFile(DialogService.FilePath);
                }
            });

        public DelegateCommand OpenFileCommand =>  new DelegateCommand(obj => 
            {
                if (DialogService.OpenFileDialog() == true)
                {
                    CurrentTabFileItem = new FileTabViewModel(FileService.Open(DialogService.FilePath), 
                        Path.GetFileName(DialogService.FilePath), DialogService.FilePath);

                    TabFileItems.Add(CurrentTabFileItem);
                }
            });

        public DelegateCommand SaveFileCommand => 
            new DelegateCommand(obj => FileService.Save(CurrentTabFileItem.FilePath, CurrentTabFileItem.TextContent));

        public DelegateCommand SaveAsFileCommand => new DelegateCommand(obj =>
            {
                if (DialogService.SaveFileDialog() == true)
                {
                    SaveFileCommand.Execute(obj);
                    FileService.SaveAs(DialogService.FilePath, CurrentTabFileItem.TextContent, Path.GetExtension(DialogService.FilePath));
                }
            });
        #endregion

        #region Application OnCloseExtraLogic
        public DelegateCommand AppClosing => new DelegateCommand(obj => { });
        #endregion
        #endregion

        #region Constructor
        public MainWindowViewModel(IDialogService dialogService, IFileIdentifier FileService)
        {
            this.DialogService = dialogService;
            this.FileIdentifier = FileService;
        }

        #endregion
    }
}
