using BITMANAGEMENT.Data;
using BITMANAGEMENT.Models;
using BITMANAGEMENT.Repository.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BITMANAGEMENT.Repository.Implementation
{
    public class AddDocumentService : IAddDocuments
    {
       
        private readonly DataContext _Context;

        public AddDocumentService(DataContext dataContext)
        {
            _Context = dataContext;
        }
        public  async Task<string> AddDocuments(AddDocumentDto documentDto)
        {
           
                if (documentDto == null)
                {
                return null;
                }

                // Map DTOs to entities
                var documentDetailsEntity = new AddDocumentDetails
                {
                    DocumentTitle = documentDto.DocumentTitle,
                    DocumentDescription = documentDto.DocumentDescription,
                    DocumentType = documentDto.DocumentType?.Select(dt => new DocumentType { DocumentName = dt.DocumentName }).ToList(),
                    Class = documentDto.Class?.Select(cd => new ClassDetails { ClassName = cd.ClassName }).ToList(),

                };

            // You may want to perform additional validation or data processing here.

            _Context.Add(documentDetailsEntity);
             await _Context.SaveChangesAsync();


            return "Document added successfully";




        }
        public async Task<IEnumerable<AddDocumentDetails>> GetAllDocuments()
        {
            // Retrieve all documents from the database using _context
            var documents = await _Context.AddDocumentDetails.ToListAsync();
            return documents;
        }
    }


    
}
