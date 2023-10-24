using Noty.Shared.FileOperations;
using System.IO;

namespace Noty.Services
{
    public class TxtFileService : FileService, IFileService
    {
        public override string Open(string path) => base.Open(path);
        public void Save(string path, string content) => base.Save(path, content);
    }
}
