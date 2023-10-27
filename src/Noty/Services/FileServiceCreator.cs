using Noty.Shared.FileOperations;
using System.IO;

namespace Noty.Services
{
    public class FileServiceCreator<TFileService> : IFileServiceCreator where TFileService : IFileService
    {
        public IFileService CreateService(string filePath) => Path.GetExtension(filePath) == ".txt" || Path.GetExtension(filePath) == null ?
            new TxtFileService(filePath) : new RtfFileService(filePath);
    }
}
