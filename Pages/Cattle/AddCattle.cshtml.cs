using DairyFarm.Data.DBContext;
using DairyFarm.Data.Entity;
using DairyFarm.Pages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DairyFarm.Pages.Cattle
{
    public class AddCattleModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AddCattleModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AddCattleClass Cow { get; set; }
        public void OnGet()
        {
            var Role = HttpContext.Session.GetString("UserRole");
            if (Role != "Owner")
            {
                Response.Redirect("/OwnerLogin");
                return;
            }

        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            var OwnerId = (int)HttpContext.Session.GetInt32("Id");
            
            var newcattle = new Cows
            {
                OwnerId = OwnerId,
                CowName = Cow.CowName,
                DOB = Cow.DOB,
                Breed = Cow.Breed,
                HealthStatus = Cow.HealthStatus,
            };
            _context.cows.Add(newcattle);
            _context.SaveChanges();
            TempData["message"] = "Cow Details Added Successfully";
            return RedirectToPage("/Cattle/CattleView");
        }
    }
}
