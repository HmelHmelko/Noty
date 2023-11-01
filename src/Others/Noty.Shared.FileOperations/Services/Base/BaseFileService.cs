using System.Text;

namespace Noty.Shared.FileOperations
{
    public class BaseFileService : IFileService
    {
        protected readonly string FilePath;
        public BaseFileService(string filePath) => this.FilePath = filePath;
        public virtual void NewFile()
        {
            using (FileStream fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write))
                fs?.Close();
        }
        public virtual string Open()
        {
            var strBuilder = new StringBuilder();

            using (StreamReader reader = new StreamReader(FilePath))
            {
                strBuilder.Append(reader.ReadToEnd());
                reader?.Close();
            }

            return strBuilder.ToString();
        }

        public async virtual void Save(string content)
        {
            using (var writer = new StreamWriter(new FileStream(FilePath, FileMode.Open, FileAccess.Write)))
            {
                await writer.WriteAsync(content).ConfigureAwait(false);
                writer?.Close();
            }
        }
        public async virtual void SaveAs(string content)
        {
            Path.ChangeExtension(FilePath, ".txt");
            using (var writer = new StreamWriter(new FileStream(FilePath, FileMode.CreateNew, FileAccess.Write)))
            {
                await writer.WriteAsync(content).ConfigureAwait(false);
                writer?.Close();
            }
        }
    }
}
