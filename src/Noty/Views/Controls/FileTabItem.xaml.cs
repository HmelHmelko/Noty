using System.ComponentModel;

namespace Noty
{
    public partial class FileTabItem : INotifyPropertyChanged
    {
        public FileTabItem()
        {
            InitializeComponent();
        }

        #region CurrentLineNumber and CurrentCharNumber

        private string currentLnNumber = "1";
        public string CurrentLnNumber
        { 
            get => currentLnNumber;
            set { currentLnNumber = value; OnPropertyChanged("CurrentLnNumber"); }        
        }

        private string currentChNumber = "1";
        public string CurrentChNumber
        { 
            get => currentChNumber;
            set { currentChNumber = value; OnPropertyChanged("CurrentChNumber"); }
        }
        public void TextArea_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {  
            var caret = TextArea.CaretIndex;
            var line = TextArea.GetLineIndexFromCharacterIndex(caret);
            var ch = TextArea.GetCharacterIndexFromLineIndex(line);

            if (line == 0) ch = caret;
            else ch = caret - TextArea.GetCharacterIndexFromLineIndex(line);
            
            

            CurrentLnNumber = (line + 1).ToString();
            CurrentChNumber = (ch + 1).ToString();
        }

        #endregion
    }
}
