using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoperasiTentera.Pages
{
    public class HomePageModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public string Name { get; set; }
        public void OnGet()
        {
        }
    }
}
