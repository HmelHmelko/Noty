using Noty.Shared.FileOperations;
using System.IO;

namespace Noty.Services
{
    public class FileIdentifier<TFileService> : IFileIdentifier where TFileService : IFileService
    {
        public IFileService IdentifyFileExtension(IDialogService dialog) => Path.GetExtension(dialog.FilePath) == ".txt" ?
            new TxtFileService() :  new RtfFileService();
    }
}
