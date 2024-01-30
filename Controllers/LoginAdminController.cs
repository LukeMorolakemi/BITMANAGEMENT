using BITMANAGEMENT.Models;
using BITMANAGEMENT.Repository.Implementation;
using BITMANAGEMENT.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BITMANAGEMENT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginAdminController : ControllerBase
    {
        private readonly ILoginAdminDto _loginAdminService;

        public LoginAdminController(ILoginAdminDto loginAdminService)
        {
            _loginAdminService = loginAdminService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginAdminDto loginDto)
        {
            // Check if the provided DTO is valid (e.g., required fields are not null)
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input");
            }

            // Authenticate the admin using the injected service
            string token = await _loginAdminService.AuthenticateAdmin(loginDto);

            if (token == null)
            {
                return Unauthorized("Invalid credentials");
            }

            // If credentials are valid, return the JWT token
            return Ok(new { Token = token });
        }
    }
}

