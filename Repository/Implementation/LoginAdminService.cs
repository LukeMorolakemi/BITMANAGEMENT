using BITMANAGEMENT.Data;
using BITMANAGEMENT.Models;
using BITMANAGEMENT.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace BITMANAGEMENT.Repository.Implementation
{
    // Implementation of LoginAdminService
    public class LoginAdminService : ILoginAdminDto
    {
        private readonly DataContext dataContext;
        private readonly IConfiguration configuration;

        public LoginAdminService(DataContext dataContext, IConfiguration configuration)
        {
            this.dataContext = dataContext;
            this.configuration = configuration;
        }

        public async Task<string> AuthenticateAdmin(LoginAdminDto loginAdmin)
        {
            // Find the admin user by email
            var adminUser = await dataContext.adminModels.FirstOrDefaultAsync(u => u.Email == loginAdmin.Email);

            if (adminUser == null)
            {
                return null; // User not found
            }

            // Compare the provided password with the hashed password stored in the database
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginAdmin.Password, adminUser.Password);

            if (!isPasswordValid)
            {
                return null; // Invalid credentials
            }

            // If credentials are valid, create and return a JWT token
            return CreateToken(adminUser);
        }

        private string CreateToken(AdminModel adminModel)
        {
            List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, adminModel.Email),
            new Claim(ClaimTypes.Role, "Admin")
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value!));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
