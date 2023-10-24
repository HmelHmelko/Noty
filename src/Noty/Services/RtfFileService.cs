using Noty.Shared.FileOperations;
using System.IO;

namespace Noty.Services
{
    public class RtfFileService : IFileService
    {
        public string Open(string path)
        {
            throw new System.NotImplementedException();
        }

        public void Save(string filePath, string content)
        {
            Path.ChangeExtension(filePath, ".rtf");
            using (StreamWriter writer = new StreamWriter(File.Create(filePath)))
            {
                writer.Write(content);
                writer.Close();
            }
        }
    }
}
