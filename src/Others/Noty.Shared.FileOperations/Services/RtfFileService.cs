using System.Text;

namespace Noty.Shared.FileOperations
{
    public class RtfFileService : BaseFileService
    {
        public RtfFileService(string filePath) : base(filePath) { }
        public override void NewFile() => base.NewFile();
        public override string Open()
        {
            //Тут должен вызывать форматтер, который преобразует файл в другое расширение
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

        public override void Save(string content) => File.WriteAllText(FilePath, content);
        public override void SaveAs(string content, string extension)
        {
            //Тут должен вызывать форматтер, который преобразует файл в другое расширение
            base.SaveAs(content, extension);
        }
    }
}
