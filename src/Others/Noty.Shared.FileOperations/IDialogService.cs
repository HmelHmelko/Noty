namespace Noty.Shared.FileOperations
{
    public interface IDialogService
    {
        void ShowMessage(string message);   // показ сообщения
        string FilePath { get; set; } // путь к выбранному файлу
        string FileExtansion { get; }
        bool OpenFileDialog();  // открытие файла
        bool SaveFileDialog();  // сохранение файла
    }
}
