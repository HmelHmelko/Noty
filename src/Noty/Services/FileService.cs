using Noty.Shared.FileOperations;
using System.IO;
using System.Text;

namespace Noty.Services
{
    public class FileService : IFileService
    {
        public virtual void NewFile(string path, string name)
        {
            lock (this)
            {
                File.Create(path);
            }
        }
        public virtual string Open(string path)
        {
            lock (this)
            {
                var strBuilder = new StringBuilder();

                using (StreamReader reader = new StreamReader(path))
                {
                    strBuilder.Append(reader.ReadToEnd());
                    reader.Close();
                }

                return strBuilder.ToString();
            }
        }

        public virtual void Save(string path, string content)
        {
            lock (this)
            {
                File.WriteAllText(path, content);
            }
        }
        public virtual void SaveAs(string fileName, string content)
        {
            throw new System.NotImplementedException();
        }
    }
}
