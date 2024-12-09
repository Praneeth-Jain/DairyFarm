using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DairyFarm.Pages.Admin
{
    public class loginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        private const string AdminUsername = "admin";
        private const string AdminPassword = "admin";
        public void OnGet()
        {
            ErrorMessage = string.Empty;

        }
        public IActionResult OnPost()
        {
            if (Username == AdminUsername && Password == AdminPassword)
            {
                
                return RedirectToPage("/Admin/Home");
            }
            else
            {
                ErrorMessage = "Invalid username or password.";
                return Page();
            }
        }


    }
}
