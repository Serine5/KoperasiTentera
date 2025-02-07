using Microsoft.AspNetCore.Mvc;

namespace KoperasiTentera.ViewModels
{
    public class CreateAccountViewModel
    {
        public string Name { get; set; }
        public int ICNumber { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}
