using Noty.Shared.FileOperations;
using System.IO;

namespace Noty.Services
{
    public class TxtFileService : FileService, IFileService
    {
        public override void NewFile(string path, string name) => base.NewFile(path, name);
        public override string Open(string path) => base.Open(path);
        public override void Save(string path, string content) => base.Save(path, content);
        public override void SaveAs(string fileName, string content) => base.SaveAs(fileName, content);
    }
}
