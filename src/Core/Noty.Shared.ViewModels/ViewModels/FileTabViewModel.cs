using System.Collections.Specialized;

namespace Noty.Shared.ViewModels
{
    public class FileTabViewModel : BaseViewModel
    {
        #region Properties
        public string TextContent { get; set; }
        public double TextFontSize { get; set; } = 14;
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool IsPinned { get; set; } = false;
        public string CurrentMethod { get; set; } = "TEST";
        #endregion

        #region Constructors
        public FileTabViewModel(string filePath, string textFromFile = "", string fileName = "Untitled.txt")
        {
            this.FilePath = filePath;
            this.TextContent = textFromFile;
            this.FileName = Path.GetFileName(filePath);
        }
        #endregion
    }
}
