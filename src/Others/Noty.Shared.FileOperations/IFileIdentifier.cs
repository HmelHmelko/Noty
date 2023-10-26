namespace Noty.Shared.FileOperations
{
    public interface IFileIdentifier
    {
        IFileService CreateService(string filePath);
    }
}
