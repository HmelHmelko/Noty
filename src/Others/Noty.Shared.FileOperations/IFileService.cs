namespace Noty.Shared.FileOperations
{
    public interface IFileService
    {
        void NewFile(string path, string name);
        string Open(string path);
        void Save(string fileName, string content);      
        void SaveAs(string fileName, string content);
    }
}
