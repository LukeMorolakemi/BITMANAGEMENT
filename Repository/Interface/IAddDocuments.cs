using BITMANAGEMENT.Models;
using BITMANAGEMENT.Repository.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace BITMANAGEMENT.Repository.Interface
{
    public interface IAddDocuments
    {
        public Task<string> AddDocuments(AddDocumentDto documentDto);
        Task<IEnumerable<AddDocumentDetails>> GetAllDocuments();
    }
}
