﻿using Noty.Models;

namespace Noty.ViewModels
{
    public class TabViewModel : BaseViewModel
    {
        public DocumentModel Document { get; private set; }
        public EditorViewModel Editor { get; private set; }
        public string TabName { get; set; }
        public bool IsPinned { get; set; }
        public double PinButtonAngle => IsPinned ? 90 : 0;
        public TabViewModel(DocumentModel document)
        {
            Document = document;
            TabName = Document.FileName;
            Editor = new EditorViewModel(document);
        }
    }
}
