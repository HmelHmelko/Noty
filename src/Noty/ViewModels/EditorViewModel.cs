using Noty.Models;
using System.Windows.Controls;

namespace Noty.ViewModels
{
    public class EditorViewModel : BaseViewModel
    {
        public DocumentModel Document { get; private set; }
        public FormatModel Format { get; private set; }

        #region Properties
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
        }
    }
}