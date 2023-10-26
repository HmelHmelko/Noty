using System.Collections.ObjectModel;
using Noty.Shared.FileOperations;

namespace Noty.Shared.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region private Properties
        private IFileIdentifier FileServiceCreator;
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
            new DelegateCommand(obj => TabFileItems.Add(CurrentTabFileItem = new FileTabViewModel()));
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
            var path = obj.ToString();
            CurrentTabFileItem = new FileTabViewModel(string.Empty, Path.GetFileName(path), path);                          
            TabFileItems.Add(CurrentTabFileItem);

            FileServiceCreator.CreateService(path).NewFile();
        });

        public DelegateCommand OpenFileCommand =>  new DelegateCommand(obj => 
        {
            var path = obj.ToString();
            CurrentTabFileItem = new FileTabViewModel(FileServiceCreator.CreateService(path).Open(), Path.GetFileName(path), path);

            TabFileItems.Add(CurrentTabFileItem);
        });

        public DelegateCommand SaveFileCommand => 
            new DelegateCommand(obj => FileServiceCreator.CreateService(CurrentTabFileItem.FilePath).Save(CurrentTabFileItem.TextContent));

        public DelegateCommand SaveAsFileCommand => new DelegateCommand(obj =>
        {
            var path = obj.ToString();
            SaveFileCommand.Execute(path);
            FileServiceCreator.CreateService(path).SaveAs(CurrentTabFileItem.TextContent, Path.GetExtension(path));
        });
        #endregion

        #region Application OnCloseExtraLogic
        public DelegateCommand AppClosing => new DelegateCommand(obj => { });
        #endregion
        #endregion

        #region Constructor
        public MainWindowViewModel(IFileIdentifier FileService)
        {
            this.FileServiceCreator = FileService;
        }

        #endregion
    }
}
