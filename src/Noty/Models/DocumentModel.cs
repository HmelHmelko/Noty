namespace Noty.Models
{
    public class DocumentModel : ObservableModel
    {
        public string TextContent { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileExtension { get; set; }
    }
}
