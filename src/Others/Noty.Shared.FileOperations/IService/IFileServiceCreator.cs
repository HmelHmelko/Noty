namespace Noty.Shared.FileOperations
{
    public interface IFileServiceCreator
    {
        IFileService CreateService(string filePath, string extension);
    }
}
