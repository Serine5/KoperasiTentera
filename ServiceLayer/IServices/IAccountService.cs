using KoperasiTenteraDAL.Entities;

namespace ServiceLayer.IServices
{
    public interface IAccountService
    {
        Task<string> CreateAccountAsync(User user);
        Task<bool> CreatePinAsync(string phoneNumber, string pin);
        Task<bool> EnableBiometricAsync(string phoneNumber, bool enable);
        Task<bool> EnablePrivacyPolicyAsync(string phoneNumber, bool enable);
        Task<string> SendOtpAsync(string phoneNumber);
        Task<string> SendEmailOtpAsync(string phoneNumber);
        Task<bool> VerifyEmailOtpAsync(string phoneNumber, string otp);
        Task<bool> VerifyOtpAsync(string phoneNumber, string otp);
    }
}