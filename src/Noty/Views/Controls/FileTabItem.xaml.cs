using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Noty
{
    public partial class FileTabItem : INotifyPropertyChanged
    {
        public FileTabItem()
        {
            InitializeComponent();
        }

        private string _currentLnNumber = "1";
        public string CurrentLnNumber
        { 
            get => _currentLnNumber;
            set 
            {   
                _currentLnNumber = value;
                OnPropertyChanged(); 
            }        
        }

        private string _currentChNumber = "1";
        public string CurrentChNumber
        {
            get => _currentChNumber;
            set { _currentChNumber = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string property = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        private void TextArea_SelectionChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            var caret = TextArea.CaretIndex;
            var line = TextArea.GetLineIndexFromCharacterIndex(caret);
            var ch = TextArea.GetCharacterIndexFromLineIndex(line);

            if (line == 0) ch = caret;
            else ch = caret - TextArea.GetCharacterIndexFromLineIndex(line);

            CurrentLnNumber = (line + 1).ToString();
            CurrentChNumber = (ch + 1).ToString();
        }
    }
}
