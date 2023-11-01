namespace Noty.Models
{
    public class DocumentModel
    {
        public string TextContent { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileExtension { get; set; }
        public bool IsEmpty => string.IsNullOrEmpty(FilePath) || string.IsNullOrEmpty(FileName) ? true : false;
    }
}
