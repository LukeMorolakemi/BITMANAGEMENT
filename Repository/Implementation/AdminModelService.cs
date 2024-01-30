using BITMANAGEMENT.Data;
using BITMANAGEMENT.Models;
using BITMANAGEMENT.Repository.Interface;

namespace BITMANAGEMENT.Repository.Implementation
{
    public class AdminModelService : IAdminModel
    {
        private readonly DataContext _dataContext;

        public AdminModelService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public AdminModel CreateAdmin(AdminModel admin)
        {
            // Perform any necessary logic (e.g., validation, hashing passwords, etc.)
            // For simplicity, we just add the admin to the DbSet in the dataContext
            _dataContext.adminModels.Add(admin);

            // Save changes to the database
            _dataContext.SaveChanges();

            // Return the created admin
            return admin;
        }
    }
}
