using BITMANAGEMENT.Models;

namespace BITMANAGEMENT.Repository.Interface
{
    public interface ILoginAdminDto
    {
        Task<string> AuthenticateAdmin(LoginAdminDto loginAdmin);
    }
}
