using System.IO;

namespace Noty.ViewModels
{
    public class FileTabViewModel : BaseViewModel
    {
        #region Properties

        #region Text
        public string TextContent { get; set; }
        public double TextFontSize { get; set; } = 14;

        #endregion

        #region File
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool IsPinned { get; set; } = false;

        #endregion

        #region Footer

        public string CurrentMethod { get; set; } = "TEST";

        #endregion

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
