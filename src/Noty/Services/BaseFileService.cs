using Noty.Shared.FileOperations;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Noty.Services
{
    public class BaseFileService : IFileService
    {
        protected readonly string FilePath;
        public BaseFileService(string filePath) => this.FilePath = filePath;
        public virtual void NewFile() => File.Create(FilePath);
        public virtual string Open()
        {
            lock (this)
            {
                var strBuilder = new StringBuilder();

                using (StreamReader reader = new StreamReader(FilePath))
                {
                    strBuilder.Append(reader.ReadToEnd());
                    reader.Close();
                }

                return strBuilder.ToString();
            }
        }

        public virtual void Save(string content) => File.WriteAllText(FilePath, content);
        public virtual void SaveAs(string content, string extension)
        {
            FileStream fs = new FileStream(FilePath, FileMode.CreateNew);
            fs.Close();
            File.WriteAllText(FilePath, content);
            Path.ChangeExtension(FilePath, extension);
        }
    }
}
