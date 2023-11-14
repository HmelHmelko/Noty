using Noty.Commands;
using Noty.Models;
using System.Windows;
using System.Windows.Input;

namespace Noty.ViewModels
{
    public class EditorViewModel : BaseViewModel
    {
        public DocumentModel Document { get; private set; }
        public FormatModel Format { get; private set; }

        #region TextControlCommands
        #region testCommand
        public ICommand testCommand { get; }
        private bool CanTestCommandExecute(object p) => true;
        private void OnTestCommandExecuted(object p)
        {
            var hello = SelectedText;
        }
        #endregion

        public ICommand CopyTextCommand { get; }
        private bool CanCopyTextCommandExecute(object p) => true;
        private void OnCopyTextCommandExecuted(object p)
        {
            Clipboard.SetText(SelectedText);
        }
        #endregion

        #region Properties
        public string SelectedText 
        { 
            get; 
            set; 
        }
        public string? TextContent
        {
            get => Document.TextContent;
            set => Document.TextContent = value;
        }
        public double FontSize
        {
            get => Format.FontSize;
            set => Format.FontSize = value;
        }
        public string? FontStyle
        {
            get => Format.FontStyle;
            set => Format.FontStyle = value;
        }
        public string? FontWeight
        {
            get => Format.FontWeight;
            set => Format.FontWeight = value;
        }
        public string? FontFamily
        {
            get => Format.FontFamily;
            set => Format.FontFamily = value;
        }
        #endregion
        public EditorViewModel(DocumentModel document)
        {
            Document = document;
            Format = new FormatModel();
            testCommand = new DelegateCommand(OnTestCommandExecuted, CanTestCommandExecute);
            CopyTextCommand = new DelegateCommand(OnCopyTextCommandExecuted, CanCopyTextCommandExecute);
        }
    }
}