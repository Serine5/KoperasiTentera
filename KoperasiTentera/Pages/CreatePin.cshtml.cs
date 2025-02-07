using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.IServices;

namespace KoperasiTentera.Pages
{
    public class CreatePinModel : PageModel
    {
        private readonly IAccountService _accountService;
        public CreatePinModel(IAccountService accountService)
        {
            _accountService = accountService;

        }

        [BindProperty(SupportsGet = true)]
        public string PhoneNumber { get; set; }


        [BindProperty(SupportsGet = true)]
        public string Pin { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            var created = await _accountService.CreatePinAsync(PhoneNumber, Pin);

            if (created)
            {
                return RedirectToPage("EnableBiometric", new { phoneNumber = PhoneNumber });
            }

            ModelState.AddModelError("", "Pin not Created");
            return Page();
        }
    }
}
