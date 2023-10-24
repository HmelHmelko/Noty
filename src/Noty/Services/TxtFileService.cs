using Microsoft.Win32;
using Noty.Shared.FileOperations;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Noty.Services
{
    public class TxtFileService : IFileService
    {
        RichTextBox textBox;
        public TxtFileService(RichTextBox richTextBox)
        {
            this.textBox = richTextBox;
        }

        public string Open(string path)
        {
            var content = string.Empty;

            StreamReader reader = new StreamReader(path);
            content = reader.ReadToEnd();
            textBox.AppendText(content);
            reader.Close();

            return content;
        }

        public void Save(string fileName, string content)
        {
            TextRange doc = new TextRange(textBox.Document.ContentStart, textBox.Document.ContentEnd);
            using (FileStream fs = File.Create(fileName))
            {
                doc.Save(fs, DataFormats.Text);
            }
        }
    }
}
