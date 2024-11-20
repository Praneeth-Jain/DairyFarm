using DairyFarm.Data.DBContext;
using DairyFarm.Pages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DairyFarm.Pages
{
    public class OwnerLoginModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        public OwnerLoginModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OwnerloginClass ownerlogin { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var getOwner = _context.owners
                          .Where(x => x.OwnerEmail == ownerlogin.OwnerEmail && x.OwnerPassword == ownerlogin.OwnerPassword)
                          .FirstOrDefault();

            if (getOwner != null)
            {
                HttpContext.Session.SetString("UserRole", "Owner");
                HttpContext.Session.SetInt32("Id", getOwner.OwnerId);
                TempData["IsLoggedIn"] = "True";
                return RedirectToPage("/Cattle/CattleView");
            }
            else
            {
                return Page();
            }
        }
    }
}
