namespace BITMANAGEMENT.Models
{
    public class AddDocumentDto
    {
        public string DocumentTitle { get; set; }

        public string DocumentDescription { get; set; }
        public List<ClassDetails> Class { get; set; }
        public List<DocumentType> DocumentType { get; set; }
    }
}
