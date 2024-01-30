using BITMANAGEMENT.Data;
using BITMANAGEMENT.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BITMANAGEMENT.Repository.Implementation
{
    public class ValidateOTPService : IValidateOTPDto
    {

        private readonly DataContext dataContext;

        public ValidateOTPService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<OTPValidationResult> ValidateOTP(string otp)
        {
            try
            {
                // Find the corresponding entry in the database using the provided OTP
                var admin = await dataContext.adminModels.FirstOrDefaultAsync(a => a.OTP == otp);

                if (admin != null)
                {
                    // Valid OTP
                    return OTPValidationResult.Success;
                }
                else
                {
                    // Invalid OTP or admin not found
                    return OTPValidationResult.InvalidOTP;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
                return OTPValidationResult.Failure;
            }
        }
    }

    // Enum to represent OTP validation result
    public enum OTPValidationResult
    {
        Success,
        InvalidOTP,
        Failure
    }
}

