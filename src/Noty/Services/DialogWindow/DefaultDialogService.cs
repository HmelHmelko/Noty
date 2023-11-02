using Microsoft.Win32;
using System.IO;

namespace Noty.Services
{
    public class DefaultDialogService : IDialogService
    {
        private readonly string _dialogFilter = "Txt files (*.txt)|*.txt|Rtf files (*rtf)|*.rtf|JSON files (*.json*)|*.json|XAML files (*.xaml)|*.xaml|All files (*.*)|*.*";
        public string? FilePath { get; private set; }
        public string? FileName { get; private set; }
        public string? FileExtension { get; private set; }

        public bool OpenFileDialog()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = _dialogFilter,
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == false) return false;

            FileInfo fileInfo = new(openFileDialog.FileName);
            FilePath = fileInfo.FullName;
            FileName = fileInfo.Name;
            FileExtension = fileInfo.Extension;
            
            return true;    
        }

        public bool SaveFileDialog()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Txt files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (saveFileDialog.ShowDialog() == false) return false;
            
            FileInfo fileInfo = new(saveFileDialog.FileName);
            FilePath = fileInfo.FullName;
            return true;

        }

        public void ShowMessage(string message)
        {
            //
        }
    }
}
