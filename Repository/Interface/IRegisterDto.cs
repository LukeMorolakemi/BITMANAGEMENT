using BITMANAGEMENT.Models;
using BITMANAGEMENT.Repository.Implementation;

namespace BITMANAGEMENT.Repository.Interface
{
    public interface IRegisterDto
    {
        Task<RegistrationResult> RegisterAdmin(RegisterDto registerDto);
    }
}
