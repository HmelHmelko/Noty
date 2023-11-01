using Noty.Models;

namespace Noty.ViewModels
{
    public class EditorViewModel : BaseViewModel
    {
        public DocumentModel Document { get; private set; }
        public FormatModel Format { get; private set; }

        public EditorViewModel(DocumentModel document)
        {
            Document = document;
            Format = new FormatModel();
        }
    }
}