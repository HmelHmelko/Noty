namespace Noty.Shared.FileOperations
{
    public interface IFileService
    {
        void NewFile();
        string Open();
        void Save(string content);      
        void SaveAs(string content);
    }
}
