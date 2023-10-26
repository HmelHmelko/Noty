using Noty.Shared.FileOperations;
using System.IO;

namespace Noty.Services
{
    public class RtfFileService : IFileService
    {
        public void NewFile(string path, string name)
        {
            throw new System.NotImplementedException();
        }

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

        public void SaveAs(string fileName, string content)
        {
            throw new System.NotImplementedException();
        }
    }
}
