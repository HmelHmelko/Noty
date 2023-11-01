using System.Text;

namespace Noty.Shared.FileOperations
{
    public class BaseFileService : IFileService
    {
        protected readonly string FilePath;
        public BaseFileService(string filePath) => this.FilePath = filePath;
        public virtual void NewFile()
        {
            File.Create(FilePath);
        }
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

        public virtual void Save(string content)
        {
            File.WriteAllText(FilePath, content);
/*            using (var writer = new StreamWriter(new FileStream(FilePath, FileMode.Create, FileAccess.Write)))
                await writer.WriteAsync(content).ConfigureAwait(false);*/
        }
        public virtual void SaveAs(string content, string extension)
        {
            FileStream fs = new FileStream(FilePath, FileMode.CreateNew);
            fs.Close();
            File.WriteAllText(FilePath, content);
            Path.ChangeExtension(FilePath, extension);
        }
    }
}
