namespace Noty.Shared.FileOperations
{
    public interface IFileService
    {
        void NewFile(string path);
        string Open(string path);
        void Save(string path, string content);      
        void SaveAs(string path, string content, string extension);
    }
}
