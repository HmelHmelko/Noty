using Noty.Shared.FileOperations;
using System.IO;

namespace Noty.Services
{
    public class FileService : IFileService
    {
        public virtual string Open(string path)
        {
            var content = string.Empty;

            using (StreamReader reader = new StreamReader(path))
            {
                content = reader.ReadToEnd();
                reader.Close();
            }

            return content;
        }

        public virtual void Save(string path, string content)
        {
            using (StreamWriter writer = new StreamWriter(File.Create(path)))
            {
                writer.Write(content);
                writer.Close();
            }
        }
    }
}
