namespace Noty.Shared.FileOperations
{
    public class FileServiceCreator<TFileService> : IFileServiceCreator where TFileService : IFileService
    {
        public IFileService CreateService(string filePath) => Path.GetExtension(filePath) == ".txt" || Path.GetExtension(filePath) == null ?
            new TxtFileService(filePath) : new RtfFileService(filePath);
    }
}
