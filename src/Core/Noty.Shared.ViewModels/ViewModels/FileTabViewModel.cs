using System.Collections.Specialized;
using System.IO;

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
        public string CurrentTextLine { get; set; } = "TEST";
        public string CurrentTextChar { get; set; } = "TEST";

/*        StringCollection GetLinesCollectionFromTextBox(string textBox)
        {
            StringCollection lines = new StringCollection();

            // lineCount may be -1 if TextBox layout info is not up-to-date.
            int lineCount = textBox.LineCount;

            for (int line = 0; line < lineCount; line++)
                // GetLineText takes a zero-based line index.
                lines.Add(textBox.GetLineText(line));

            return lines;
        }*/

        #endregion

        #region Constructors
        public FileTabViewModel() => FileName = "Untitled.txt";
        public FileTabViewModel(string textFromFile, string filePath)
        {
            this.TextContent = textFromFile;
            this.FileName = Path.GetFileName(filePath);
            this.FilePath = filePath;
        }
        #endregion
    }
}
