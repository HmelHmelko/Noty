using Noty.Shared.FileOperations;
using System.IO;
using System.Text;

namespace Noty.Services
{
    public class TxtFileService : IFileService
    {
        public void NewFile(string path) => File.Create(path);
        public string Open(string path)
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

        public  void Save(string path, string content) => File.WriteAllText(path, content);

        public void SaveAs(string path, string content, string extension)
        {
            throw new System.NotImplementedException();
        }
    }
}
