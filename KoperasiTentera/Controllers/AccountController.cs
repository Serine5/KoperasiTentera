using KoperasiTenteraDAL.Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.IServices;

namespace KoperasiTentera.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpPost("create-account")]
        public async Task<IActionResult> CreateAccount([FromBody] User user)
        {
            try
            {
                var phoneNumber = await _service.CreateAccountAsync(user);
                return Ok(new { Message = "Account created. Proceed to OTP verification.", PhoneNumber = phoneNumber });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOtp([FromBody] string phoneNumber)
        {
            try
            {
                var result = await _service.SendOtpAsync(phoneNumber);
                return Ok(new { Message = "OTP sent successfully.", Result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("send-email-otp")]
        public async Task<IActionResult> SendEmailOtp([FromBody] string phoneNumber)
        {
            try
            {
                var result = await _service.SendEmailOtpAsync(phoneNumber);
                return Ok(new { Message = "OTP sent successfully.", Result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] string phoneNumber, string otp)
        {
            try
            {
                var isValid = await _service.VerifyOtpAsync(phoneNumber, otp);
                if (isValid)
                    return Ok(new { Message = "OTP verified. Proceed to Email OTP verification." });
                return BadRequest(new { Error = "Invalid OTP." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("verify-email-otp")]
        public async Task<IActionResult> VerifyEmailOtp([FromBody] string phoneNumber, string otp)
        {
            try
            {
                var isValid = await _service.VerifyEmailOtpAsync(phoneNumber, otp);
                if (isValid)
                    return Ok(new { Message = "Email OTP verified. Proceed to Privacy Policy agreement." });
                return BadRequest(new { Error = "Invalid Email OTP." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("agree-policy")]
        public IActionResult AgreePolicy()
        {
            return Ok(new { Message = "Privacy Policy agreed. Proceed to PIN creation." });
        }

        [HttpPost("create-pin")]
        public async Task<IActionResult> CreatePin([FromBody] string phoneNumber, string pin)
        {
            try
            {
                await _service.CreatePinAsync(phoneNumber, pin);
                return Ok(new { Message = "PIN created successfully. Proceed to biometric setup." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("enable-biometric")]
        public async Task<IActionResult> EnableBiometric([FromBody] string phoneNumber, bool enable)
        {
            try
            {
                await _service.EnableBiometricAsync(phoneNumber, enable);
                return Ok(new { Message = enable ? "Biometric enabled successfully." : "Biometric setup skipped." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("enable-privacy-policy")]
        public async Task<IActionResult> EnablePrivacyPolicy([FromBody] string phoneNumber, bool enable)
        {
            try
            {
                await _service.EnablePrivacyPolicyAsync(phoneNumber, enable);
                return Ok(new { Message = enable ? "PrivacyPolicy enabled successfully." : "PrivacyPolicy setup skipped." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
