using BITMANAGEMENT.Models;
using Microsoft.EntityFrameworkCore;

namespace BITMANAGEMENT.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<AdminModel> adminModels { get; set; }
        public DbSet<ClassDetails> ClassDetails { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<AddDocumentDetails> AddDocumentDetails { get; set; }
    }
}
