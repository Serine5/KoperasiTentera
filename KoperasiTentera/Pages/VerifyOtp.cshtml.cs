using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.IServices;

namespace KoperasiTentera.Pages
{
    public class VerifyOtpModel : PageModel
    {
        private readonly IAccountService _accountService;

        [BindProperty(SupportsGet = true)]
        public string PhoneNumber { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Otp { get; set; }
        public VerifyOtpModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _accountService.SendOtpAsync(PhoneNumber);
            var isverified = await _accountService.VerifyOtpAsync(PhoneNumber, Otp);

            if (isverified)
            {
                return RedirectToPage("VerifyEmailOtp", new { phoneNumber = PhoneNumber });
            }

            ModelState.AddModelError("", "Invalid OTP.");
            return Page();
        }

    }
}
