using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DairyFarm.Data.DBContext;
using DairyFarm.Data.Entity;
using DairyFarm.Model;

namespace DairyFarm.Pages
{
    public class OwnerRegisterModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        public OwnerRegisterModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public OwnerRegisterClass reg { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var newowner = new Owners
            {
                OwnerName = reg.OwnerName,
                OwnerEmail = reg.OwnerEmail,
                OwnerPhone = reg.OwnerPhone,
                OwnerPassword = reg.OwnerPassword,

            };

            _context.owners.Add(newowner);
            _context.SaveChanges();

            var thisUser=_context.owners.Where(x=>x.OwnerEmail == reg.OwnerEmail).FirstOrDefault();
            var freesubs = new Subscriptions
            {
                OwnerId = thisUser.OwnerId,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddYears(3),
                Status = "Active",
                Tier = "free"
            };
            _context.subscriptions.Add(freesubs);
            _context.SaveChanges();
            return RedirectToPage("OwnerLogin");
        }
    }
}
