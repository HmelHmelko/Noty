using System.ComponentModel;
using System.Windows.Input;

namespace Noty
{
    public partial class FileTabItem : INotifyPropertyChanged
    {
        public FileTabItem()
        {
            InitializeComponent();
        }

        private string currentLnNumber;
        public string CurrentLnNumber
        { 
            get => currentLnNumber;
            set { currentLnNumber = value; OnPropertyChanged("CurrentLnNumber"); }        
        }

        private string currentChNumber;
        public string CurrentChNumber
        { 
            get => currentChNumber;
            set { currentChNumber = value; OnPropertyChanged("CurrentChNumber"); }
        }
        private void TextArea_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var chNumberFromMousePos = TextArea.GetCharacterIndexFromPoint(e.GetPosition(this), true);
            var line = TextArea.GetLineIndexFromCharacterIndex(chNumberFromMousePos);
            var ch = TextArea.GetCharacterIndexFromLineIndex(line);

            if (line == 0) ch = chNumberFromMousePos;
            else ch = chNumberFromMousePos - TextArea.GetCharacterIndexFromLineIndex(line);

            CurrentLnNumber = (line + 1).ToString();
            CurrentChNumber = (ch + 1).ToString();
        }
    }
}
