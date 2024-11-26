using DairyFarm.Data.DBContext;
using DairyFarm.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DairyFarm.Pages.Subscription
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            
          
        }

        public IActionResult OnPostSubscribe(string selectedTier) {
            if(selectedTier != null)
            {
                var userId =(int) HttpContext.Session.GetInt32("Id");
                var today= DateTime.Now;
                var endday= today.AddYears(1);
                var status = "Active";

                var User=_context.subscriptions.Where(s=>s.OwnerId==userId).FirstOrDefault();
                if (User != null)
                {
                    var existing_tier = User.Tier;
                    var validation =
                    (existing_tier == "free" && (selectedTier == "standard" || selectedTier == "premium")) ||
                    (existing_tier == "standard" && selectedTier == "premium");
                    if(!validation) {
                        TempData["SubMessage"] = "Subscription is already active till   " + User.EndDate.Date.ToString("d"); 
                        return Page();
                    }
                    User.StartDate = today;
                    User.EndDate = endday;
                    User.Status = status;
                    User.Tier = selectedTier;
                    _context.SaveChanges();
                    return RedirectToPage("/Index");
                    
                }
            }

            return Page();
            
        }
    }
}
