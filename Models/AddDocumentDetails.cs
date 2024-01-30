namespace BITMANAGEMENT.Models
{
    public class ClassDetails
    {
        public int Id { get; set; }
        public string ClassName { get; set; }

    }


    public class DocumentType
    {
        public int Id { get; set; }
        public string DocumentName { get; set; }

    }

    public class AddDocumentDetails
    {
        public int Id { get; set; }
        public string DocumentTitle { get; set; }

        public string DocumentDescription { get; set; }
        public List<ClassDetails> Class { get; set; }
        public List<DocumentType> DocumentType { get; set; }

    }

}
