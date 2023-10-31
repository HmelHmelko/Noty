using Noty.Models;

namespace Noty.ViewModels
{
    public class TabViewModel
    {
        public DocumentModel Document { get; private set; }
        public EditorViewModel Editor { get; private set; }
        public string TabName { get; set; }

        public TabViewModel(DocumentModel document)
        {
            Document = document;
            TabName = Document.FileName;
            Editor = new EditorViewModel(document);
        }
    }
}
