using DairyFarm.Data.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DairyFarm.Pages.Admin
{
    public class StatsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public StatsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public decimal TotalIncome { get; set; }
        public int TotalOwners { get; set; }
        public int TotalCows { get; set; }
        public List<string> SubscriptionsTaken { get; set; }

        public void OnGet()
        {
            TotalIncome = _context.incomes.Sum(i => i.Amount);

            TotalOwners = _context.owners.Count();

            TotalCows = _context.cows.Count();

           
        }
    }
}
