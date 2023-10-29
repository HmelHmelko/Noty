using System.ComponentModel;
using System.Windows.Documents;
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
            set 
            {   
                currentLnNumber = value;
                OnPropertyChanged("CurrentLnNumber"); 
            }        
        }

        private string currentChNumber;
        public string CurrentChNumber
        { 
            get => currentChNumber;
            set { currentChNumber = value; OnPropertyChanged("CurrentChNumber"); }
        }
        private void TextArea_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var caret = TextArea.CaretIndex;
            //var chNumberFromMousePos = TextArea.GetCharacterIndexFromPoint(Mouse.GetPosition(this), true);
            var line = TextArea.GetLineIndexFromCharacterIndex(caret);
            var ch = TextArea.GetCharacterIndexFromLineIndex(line);

            if (line == 0) ch = caret;
            else ch = caret - TextArea.GetCharacterIndexFromLineIndex(line);

            CurrentLnNumber = (line + 1).ToString();
            CurrentChNumber = (ch + 1).ToString();
        }
    }
}
