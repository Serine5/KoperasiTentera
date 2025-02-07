using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.IServices;

namespace KoperasiTentera.Pages
{
    public class PrivacyPolicyModel : PageModel
    {
        private readonly IAccountService _accountService;
        public PrivacyPolicyModel(IAccountService accountService)
        {
            _accountService = accountService;

        }

        [BindProperty(SupportsGet = true)]
        public string PhoneNumber { get; set; }


        [BindProperty(SupportsGet = true)]
        public bool Policy { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            var enable = await _accountService.EnablePrivacyPolicyAsync(PhoneNumber, Policy);

            if (enable)
            {
                return RedirectToPage("CreatePin", new { phoneNumber = PhoneNumber});
            }

            ModelState.AddModelError("", "Privacy Policy not Confirmed");
            return Page();
        }
    }
}
