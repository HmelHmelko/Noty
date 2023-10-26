using Microsoft.Win32;
using Noty.Shared.FileOperations;

namespace Noty.Services
{
    public class DefaultDialogService : IDialogService
    {
        private string dialogFilter = "txt files (*.txt)|*.txt|rtf files (*rtf)|*.rtf|All files (*.*)|*.*";
        public string FilePath { get; private set; }        
        public bool OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = dialogFilter;
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }

        public bool SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = dialogFilter;
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
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
