using Microsoft.Win32;
using System.IO;

namespace Noty.Services
{
    public class DefaultDialogService : IDialogService
    {
        private string dialogFilter = "Txt files (*.txt)|*.txt|Rtf files (*rtf)|*.rtf|JSON files (*.json*)|*.json|XAML files (*.xaml)|*.xaml|All files (*.*)|*.*";
        public string FilePath { get; private set; }
        public string FileName { get; private set; }
        public string FileExtension { get; private set; }

        public bool OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = dialogFilter;
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                FilePath = fileInfo.FullName;
                FileName = fileInfo.Name;
                FileExtension = fileInfo.Extension;

                return true;
            }
            return false;
        }

        public bool SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == true)
            {
                FileInfo fileInfo = new FileInfo(saveFileDialog.FileName);
                FilePath = fileInfo.FullName;
                return true;
            }
            return false;
        }

        public void ShowMessage(string message)
        {
            //
        }
    }
}
