using Noty.Shared.FileOperations;
using System.IO;

namespace Noty.Services
{
    public class FileIdentifier<TFileService> : IFileIdentifier where TFileService : IFileService
    {
        public IFileService CreateService(string filePath) => Path.GetExtension(filePath) == ".txt" ?
            new TxtFileService(filePath) : new RtfFileService(filePath);
    }
}
