using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Noty
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool match = true;
        string filePath = string.Empty;
        string titleName = string.Empty;
        string documentContent = string.Empty;

        string DocumentContent 
        { 
            get { return documentContent; } 
            set { documentContent = value; } 
        }

        string TitleName
        {
            get { return titleName; }
            set { titleName = value; }
        }

        string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        bool Match
        {
            get { return match; }
            set { match = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenMenuItemClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog();
        }

        private void OpenFileDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            
            if(ofd.FileName != null)
            {
                //Path to document
                FilePath = ofd.FileName;

                //Document name
                TitleName = ofd.SafeFileName;

                //if the document matches the textbox
                Match = true;

                StreamReader fileReader = new StreamReader(FilePath);
                DocumentContent = fileReader.ReadToEnd();;
                textArea.AppendText(documentContent);
            }
        }
    }
}
