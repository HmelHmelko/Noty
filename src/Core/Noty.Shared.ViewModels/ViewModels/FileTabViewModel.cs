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

        #endregion

        #region Constructors
        public FileTabViewModel() => FileName = "Untitled";
        public FileTabViewModel(string textFromFile, string fileName, string filePath)
        {
            this.TextContent = textFromFile;
            this.FileName = fileName;
            this.FilePath = filePath;
        }
        #endregion
    }
}
