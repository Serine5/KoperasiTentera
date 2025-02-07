using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.IServices;

namespace KoperasiTentera.Pages
{
    public class VerifyEmailOtpModel : PageModel
    {
        private readonly IAccountService _accountService;

        [BindProperty(SupportsGet = true)]
        public string PhoneNumber { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Otp { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Email { get; set; }
        public VerifyEmailOtpModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var email = await _accountService.SendEmailOtpAsync(PhoneNumber);
            var isverified = await _accountService.VerifyEmailOtpAsync(PhoneNumber, Otp);
            Email = email;

            if (isverified)
            {
                return RedirectToPage("PrivacyPolicy", new { phoneNumber = PhoneNumber, email = Email });
            }

            ModelState.AddModelError("", "Invalid OTP.");
            return Page();
        }

    }
}
