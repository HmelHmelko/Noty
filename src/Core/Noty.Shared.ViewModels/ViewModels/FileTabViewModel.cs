using System.Collections.ObjectModel;

namespace Noty.Shared.ViewModels
{
    public class FileTabViewModel : BaseViewModel
    {
        public readonly MainWindowViewModel FileTabsParent;

        #region Properties
        public string TextContent { get; set; }
        public double TextFontSize { get; set; } = 14;
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool IsPinned { get; set; } = false;
        public ObservableCollection<TextViewModel> TextLinesContent { get; set; } = new ObservableCollection<TextViewModel>();
        public TextViewModel CurrentTextLineContent { get; set; }

        #endregion

        public FileTabViewModel(MainWindowViewModel parent, string textFromFile)
        {
            FileName = "Untitled";
            TextContent = textFromFile;

            FileTabsParent = parent;

            TextLinesContent.Add(new TextViewModel());
            CurrentTextLineContent = TextLinesContent.FirstOrDefault();
        }
    }

    public class TextViewModel : BaseViewModel
    {
        public string TextContent { get; set; }
        public TextViewModel()
        {
            TextContent = "HELLO WORLD";
        }
    }
}
