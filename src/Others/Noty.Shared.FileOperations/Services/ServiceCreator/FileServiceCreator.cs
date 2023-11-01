namespace Noty.Shared.FileOperations
{
    public class FileServiceCreator<TFileService> : IFileServiceCreator where TFileService : IFileService
    {
        public IFileService CreateService(string filePath, string extension) => extension == ".txt" || extension == null ?
            new TxtFileService(filePath) : new RtfFileService(filePath);
    }
}
