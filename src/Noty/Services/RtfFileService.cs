using Noty.Shared.FileOperations;
using System.IO;

namespace Noty.Services
{
    public class RtfFileService : IFileService
    {
        public void NewFile(string path)
        {
            throw new System.NotImplementedException();
        }

        public string Open(string path)
        {
            throw new System.NotImplementedException();
        }

        public void Save(string filePath, string content) => File.WriteAllText(filePath, content);
        public void SaveAs(string path, string content, string extension)
        {
            //Тут должен вызывать форматтер, который преобразует файл в другое расширение
            FileStream fs = new FileStream(path, FileMode.CreateNew);
            fs.Close();
            File.WriteAllText(path, content);
            Path.ChangeExtension(path, extension);
        }
    }
}
