using KoperasiTenteraDAL.Context;
using KoperasiTenteraDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ServiceLayer.ExceptionHandlerHelper;
using ServiceLayer.IServices;
using System.Text.RegularExpressions;

namespace ServiceLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserContext _context;
        private readonly IMemoryCache _cache;
        private readonly Random _random = new();

        public AccountService(UserContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<string> CreateAccountAsync(User user)
        {
            IsValidPhone(user.PhoneNumber);
            IsValidEmail(user.Email);

            if (await _context.Users.AnyAsync(u => u.Email == user.Email || u.PhoneNumber == user.PhoneNumber))
            {
                throw AccountException.Create(AccountErrorCodes.AccountAlreadyExists, "Account already exists");
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            _cache.Set($"email_{user.PhoneNumber}", user.Email, TimeSpan.FromMinutes(10));

            return user.PhoneNumber;
        }

        public async Task<string> SendOtpAsync(string phoneNumber)
        {
            string otp = _random.Next(1000, 9999).ToString();
            _cache.Set($"otp_{phoneNumber}", otp, TimeSpan.FromMinutes(5));
            return await Task.FromResult("OTP sent to phone");
        }

        public async Task<bool> VerifyOtpAsync(string phoneNumber, string otp)
        {
            if (!_cache.TryGetValue($"otp_{phoneNumber}", out string storedOtp) || storedOtp != otp)
            {
                throw AccountException.Create(AccountErrorCodes.InvalidOtp, "Invalid OTP");
            }

            _cache.Remove($"otp_{phoneNumber}");

            if (!_cache.TryGetValue($"email_{phoneNumber}", out string email))
            {
                throw AccountException.Create(AccountErrorCodes.UserNotFound, "Email not found in cache");
            }

            return await Task.FromResult(true);
        }

        public async Task<string> SendEmailOtpAsync(string phoneNumber)
        {
            if (!_cache.TryGetValue($"email_{phoneNumber}", out string email))
            {
                throw AccountException.Create(AccountErrorCodes.UserNotFound, "Email not found in cache");
            }

            string otp = _random.Next(1000, 9999).ToString();
            _cache.Set($"email_otp_{email}", otp, TimeSpan.FromMinutes(5));

            return await Task.FromResult(email);
        }

        public async Task<bool> VerifyEmailOtpAsync(string phoneNumber, string otp)
        {
            if (!_cache.TryGetValue($"email_{phoneNumber}", out string email))
            {
                throw AccountException.Create(AccountErrorCodes.UserNotFound, "Email not found in cache");
            }

            if (!_cache.TryGetValue($"email_otp_{email}", out string storedOtp) || storedOtp != otp)
            {
                throw AccountException.Create(AccountErrorCodes.InvalidOtp, "Invalid Email OTP");
            }

            _cache.Remove($"email_otp_{email}");
            _cache.Remove($"email_{phoneNumber}");

            return await Task.FromResult(true);
        }

        public async Task<bool> CreatePinAsync(string phoneNumber, string pin)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);

            if (user == null)
            {
                throw AccountException.Create(AccountErrorCodes.UserNotFound, "User not found");
            }

            if (int.TryParse(pin, out _) && pin.Length != 6)
            {
                throw AccountException.Create(AccountErrorCodes.PinMismatch, "Pin is not correct");
            }

            user.Pin = pin;
            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public async Task<bool> EnableBiometricAsync(string phoneNumber, bool enable)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
            if (user == null)
            {
                throw AccountException.Create(AccountErrorCodes.UserNotFound, "User not found");
            }

            user.BiometricEnabled = enable;
            await _context.SaveChangesAsync();

            return await Task.FromResult(enable);
        }

        public async Task<bool> EnablePrivacyPolicyAsync(string phoneNumber, bool enable)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
            if (user == null)
            {
                throw AccountException.Create(AccountErrorCodes.UserNotFound, "User not found");
            }

            user.PrivacyPolicyEnabled = enable;
            await _context.SaveChangesAsync();

            return await Task.FromResult(enable);
        }

        private static void IsValidPhone(string phoneNumber)
        {
            var regex = new Regex("^\\+?\\d{1,4}?[-.\\s]?\\(?\\d{1,3}?\\)?[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,9}$");

            if (!regex.IsMatch(phoneNumber))
            {
                throw AccountException.Create(AccountErrorCodes.InvalidPhoneNumber, "Invalid phone number");
            }
        }

        private static void IsValidEmail(string email)
        {
            var regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
                                    RegexOptions.Compiled | RegexOptions.IgnoreCase);
            if (!regex.IsMatch(email))
            {
                throw AccountException.Create(AccountErrorCodes.InvalidEmail, "Invalid email");
            }
        }
    }
}
