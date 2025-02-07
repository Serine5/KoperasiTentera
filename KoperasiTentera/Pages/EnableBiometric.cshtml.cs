using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.IServices;

namespace KoperasiTentera.Pages
{
    public class EnableBiometric : PageModel
    {
        private readonly IUserService _userService;
        public EnableBiometric(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty(SupportsGet = true)]
        public string PhoneNumber { get; set; }


        [BindProperty(SupportsGet = true)]
        public bool Enable { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            var userName = _userService.GetAllUsersAsync().Result.FirstOrDefault(u => u.PhoneNumber == PhoneNumber).Name;

            if (!userName.IsNullOrEmpty())
            {
                return RedirectToPage("HomePage", new { name = userName });
            }

            return Page();
        }
    }
}
