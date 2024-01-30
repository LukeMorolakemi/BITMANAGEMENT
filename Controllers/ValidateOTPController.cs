using BITMANAGEMENT.Models;
using BITMANAGEMENT.Repository.Implementation;
using BITMANAGEMENT.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BITMANAGEMENT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidateOTPController : ControllerBase
    {
        private readonly IValidateOTPDto _validateOTPService;

        public ValidateOTPController(IValidateOTPDto validateOTPService)
        {
            _validateOTPService = validateOTPService;
        }

        [HttpPost("validate")]
        public async Task<IActionResult> ValidateOTP(ValidateOTPDto validateOTPDTO)
        {
            if (validateOTPDTO == null)
            {
                return BadRequest("Invalid OTP data");
            }

            // Use the injected service to validate the OTP
            var result = await _validateOTPService.ValidateOTP(validateOTPDTO.OTP);

            switch (result)
            {
                case OTPValidationResult.Success:
                    return Ok("OTP validated successfully!");
                case OTPValidationResult.InvalidOTP:
                    return BadRequest("Invalid OTP or admin not found");
                case OTPValidationResult.Failure:
                default:
                    return StatusCode(500, "An error occurred while validating OTP");
            }
        }
    }

}
