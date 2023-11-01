using Noty.Models;
using System.Windows.Controls;

namespace Noty.ViewModels
{
    public class TabViewModel : BaseViewModel
    {
        public DocumentModel Document { get; private set; }
        public EditorViewModel Editor { get; private set; }
        public string TabName { get; set; }
        public bool IsPinned { get; set; }
        public double PinButtonAngle => IsPinned ? 90 : 0;
        public string CurrentLnNumber { get; set; } = "1";
        public string CurrentChNumber { get; set; } = "1";

        public TextBox TextArea { get; set; }
        public TabViewModel(DocumentModel document)
        {
            Document = document;
            TabName = Document.FileName;
            Editor = new EditorViewModel(document);
        }
    }
}
