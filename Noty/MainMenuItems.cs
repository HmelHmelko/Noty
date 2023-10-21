using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Noty
{
    public class MainMenuItems : INotifyPropertyChanged
    {
        string DocumentContent { get; set; }    
        string TitleName { get; set; }  
        string FilePath { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void OpenFile(RichTextBox textArea)
        {
            textArea.Document.Blocks.Clear();
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == true)
            {
                //Path to document
                FilePath = ofd.FileName;
                //Document name
                TitleName = ofd.SafeFileName;

                StreamReader fileReader = new StreamReader(FilePath);
                DocumentContent = fileReader.ReadToEnd(); ;
                textArea.AppendText(DocumentContent);
            }
        }

        public void NewFile(RichTextBox textArea) => textArea.Document.Blocks.Clear();

        public void SaveFile(RichTextBox textArea)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == true)
            {
                TextRange doc = new TextRange(textArea.Document.ContentStart, textArea.Document.ContentEnd);
                using (FileStream fs = File.Create(sfd.FileName))
                {
                    doc.Save(fs, DataFormats.Text);
                }
            }
        }

    }
}
