using BITMANAGEMENT.Models;
using BITMANAGEMENT.Repository.Implementation;
using BITMANAGEMENT.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BITMANAGEMENT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterAdminController : ControllerBase
    {
        private readonly IRegisterDto _registerAdminService;

        public RegisterAdminController(IRegisterDto registerAdminService)
        {
            _registerAdminService = registerAdminService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (registerDto == null)
            {
                return BadRequest("Invalid user data");
            }

            // Use the injected service to register the admin
            var result = await _registerAdminService.RegisterAdmin(registerDto);

            switch (result)
            {
                case RegistrationResult.Success:
                    return Ok("User registered successfully. OTP sent to email.");
                case RegistrationResult.EmailAlreadyExists:
                    return BadRequest("Email already exists");
                case RegistrationResult.TooManyAdmins:
                    return BadRequest("Cannot add more than 10 admins");
                case RegistrationResult.Failure:
                default:
                    return StatusCode(500, "Failed to register user");
            }
        }
    }

}
