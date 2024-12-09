using DairyFarm.Data.DBContext;
using DairyFarm.Data.Entity;
using DairyFarm.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DairyFarm.Pages.Admin
{
    public class HomeModel : PageModel
    {

        private readonly ApplicationDbContext _context;


        public HomeModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<AdminViewModel> Admins { get; set; }
        public void OnGet()
        {
            Admins = _context.owners
                .Select(owner => new AdminViewModel
                {
                    OwnerName = owner.OwnerName,
                    CowCount = _context.cows.Count(c => c.OwnerId == owner.OwnerId),
                    SubscriptionTier = _context.subscriptions
                        .Where(s => s.OwnerId == owner.OwnerId && s.Status == "Active")
                        .Select(s => s.Tier)
                        .FirstOrDefault() ?? "No Subscription"
                })
                .ToList();
        }
    }
}
