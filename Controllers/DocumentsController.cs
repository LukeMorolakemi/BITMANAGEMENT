using System.Threading.Tasks;
using BITMANAGEMENT.Models;
using BITMANAGEMENT.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BITMANAGEMENT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddDocumentController : ControllerBase
    {
        private readonly IAddDocuments _addDocumentService;

        public AddDocumentController(IAddDocuments addDocumentService)
        {
            _addDocumentService = addDocumentService;
        }

        [HttpPost]
        [Route("Add_Documents")]
        public async Task<IActionResult> AddDocument([FromBody] AddDocumentDto documentDto)
        {
            try
            {
                var result = await _addDocumentService.AddDocuments(documentDto);
                return Ok(result);
               
            }
            catch 
            {
                // Log the exception or handle it as needed
                return StatusCode(500, "Document Could not be Added");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDocuments()
        {
            try
            {
                // Retrieve all documents from the service
                var documents = await _addDocumentService.GetAllDocuments();

                // Return the list of documents as JSON
                return Ok(documents);
            }
            catch 
            {
                // Log the exception or handle it as needed
                return StatusCode(500, "Could not retrieve Documents");
            }
        }
    }
}
