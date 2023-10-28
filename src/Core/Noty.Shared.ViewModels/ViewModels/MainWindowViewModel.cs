using System.Collections.ObjectModel;
using Noty.Shared.FileOperations;

namespace Noty.Shared.ViewModels
{
    /// <summary> This class receives DataContext from MainWindow
    /// The main goal is to process the received information and provide the result of that process
    /// Processing and providing is determined by two processes:
    /// 1. Creating correct FileTabItem objects by providing correct info from FileServices by delegateCommands
    /// 2. Observing FileTabItem objects by ObservableCollection (TabFileItems) (observing changes in their properties)
    /// Also class provide info about current active fileTab;
    /// </summary>
    public class MainWindowViewModel : BaseViewModel
    {
        #region private Properties
        private IFileServiceCreator FileServiceCreator; //Specify the required service
        private IFileService FileService => FileServiceCreator.CreateService(CurrentTabFileItem.FilePath); //Get this service (create object)

        /// <summary>
        /// Logic for correct arrangement of TabItems in TabControl
        /// </summary>
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

        //Observe FileTabItem objects
        public ObservableCollection<FileTabViewModel> TabFileItems { get; set; } =
            new ObservableCollection<FileTabViewModel>();
        public FileTabViewModel CurrentTabFileItem { get; set; }
        #endregion

        /// <summary>
        /// Commands that handle user interaction with menus that require additional logic
        /// it all commands that require FileService objects or set changes in FileTabItem properties directly
        /// or must be executed during global application processes (app closing for example)
        /// </summary>
        #region DelegateCommands
        #region Tabs
        public DelegateCommand AddTabItemCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    var untitledCount = 0;
                    var fileName = $"Untitled.txt";
                    var filePath = Directory.GetParent(Directory.GetCurrentDirectory()).ToString() + "\\";

                    while (File.Exists(filePath + fileName))
                    {
                        untitledCount++;
                        fileName = $"Untitled ({untitledCount}).txt";
                    }

                    NewFileCommand.Execute(filePath + fileName);
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
        public DelegateCommand NewFileCommand => new DelegateCommand(obj =>
        {
            var path = obj.ToString();
            CurrentTabFileItem = new FileTabViewModel(path);                          
            TabFileItems.Add(CurrentTabFileItem);

            FileService.NewFile();
        });

        public DelegateCommand OpenFileCommand =>  new DelegateCommand(obj => 
        {
            var path = obj.ToString();
            CurrentTabFileItem = new FileTabViewModel(path);
            CurrentTabFileItem.TextContent = FileService.Open();

            TabFileItems.Add(CurrentTabFileItem);
        });

        public DelegateCommand SaveFileCommand => 
            new DelegateCommand(obj => FileService.Save(CurrentTabFileItem.TextContent));

        public DelegateCommand SaveAsFileCommand => new DelegateCommand(obj =>
        {
            var path = obj.ToString();
            SaveFileCommand.Execute(path);
            FileService.SaveAs(CurrentTabFileItem.TextContent, Path.GetExtension(path));
        });
        #endregion

        #region Application OnCloseExtraLogic
        public DelegateCommand AppClosing => new DelegateCommand(obj => { });
        #endregion
        #endregion

        #region Constructor
        public MainWindowViewModel(IFileServiceCreator fileServiceCreator)
        {
            this.FileServiceCreator = fileServiceCreator;
        }

        #endregion
    }
}
