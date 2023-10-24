namespace Noty.Shared.FileOperations
{
    public interface IFileService
    {
        string Open(string path);
        void Save(string fileName, string content);
    }
}
