using KoperasiTentera.ViewModels;
using KoperasiTenteraDAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.IServices;

namespace KoperasiTentera.Pages
{
    public class CreateAccountModel : PageModel
    {
        private readonly IAccountService _accountService;

        [BindProperty]
        public CreateAccountViewModel Account { get; set; } = new();

        public CreateAccountModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var phonenumber = await _accountService.CreateAccountAsync(new User
            {
                Name = Account.Name,
                ICNumber = Account.ICNumber,
                PhoneNumber = Account.PhoneNumber,
                Email = Account.Email
            });

            if (phonenumber == Account.PhoneNumber)
            {
                return RedirectToPage("VerifyOtp", new { phoneNumber = Account.PhoneNumber });
            }
            return Page();
        }
    }
}
