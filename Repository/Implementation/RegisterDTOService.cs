using BITMANAGEMENT.Data;
using BITMANAGEMENT.Models;
using BITMANAGEMENT.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using System.Text;
using static System.Net.WebRequestMethods;

namespace BITMANAGEMENT.Repository.Implementation
{


    public class RegisterDTOservice : IRegisterDto
    {
        private readonly DataContext dataContext;

        public RegisterDTOservice(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<RegistrationResult> RegisterAdmin(RegisterDto registerDto)
        {
            try
            {
                // Check if the email already exists in the database
                if (await dataContext.adminModels.AnyAsync(u => u.Email == registerDto.Email))
                {
                    return RegistrationResult.EmailAlreadyExists;
                }

                // Check the count of existing admins
                int adminCount = await dataContext.adminModels.CountAsync();

                if (adminCount >= 10)
                {
                    return RegistrationResult.TooManyAdmins;
                }

                // Here you would typically hash the password before saving it
                // For example, using BCrypt.Net:
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
                registerDto.Password = hashedPassword;

                var adminModel = new AdminModel
                {
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    OTP = GenerateOTP(), // Generate OTP here
                    Email = registerDto.Email,
                    Password = registerDto.Password // Already hashed
                };

                dataContext.adminModels.Add(adminModel);
                await dataContext.SaveChangesAsync();

                // Send OTP via Email asynchronously
                await SendOtpEmail(adminModel.Email, adminModel.OTP);

                return RegistrationResult.Success;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return RegistrationResult.Failure;
            }
        }

        private string GenerateOTP()
        {
            Random rand = new Random();
            string otp = rand.Next(100000, 999999).ToString();
            return otp;
        }

        private async Task SendOtpEmail(string email, string otp)
        {
            try
            {

                using MailMessage mail = new MailMessage();
                {
                    mail.From = new MailAddress("lukemorolakemi@gmail.com");
                    mail.To.Add(email);
                    mail.Subject = "OTP REGISTRATION CODE";
                    mail.Body = $"Your OTP for registration is: {otp}";

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com"))
                    {
                        smtp.Port = 587;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("lukemorolakemi@gmail.com", "qcyz ypqk pslv mlmt");
                        smtp.EnableSsl = true;


                        await smtp.SendMailAsync(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to send OTP: {ex.Message}");
            }
        }
    }

    // Enum to represent registration result
    public enum RegistrationResult
    {
        Success,
        EmailAlreadyExists,
        TooManyAdmins,
        Failure
    }

}
