using BITMANAGEMENT.Repository.Implementation;

namespace BITMANAGEMENT.Repository.Interface
{
    public interface IValidateOTPDto
    {
        Task<OTPValidationResult> ValidateOTP(string otp);
    }
}
