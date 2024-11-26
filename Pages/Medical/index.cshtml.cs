using DairyFarm.Data.DBContext;
using DairyFarm.Data.Entity;
using DairyFarm.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DairyFarm.Pages.Medical
{
    [Authorize(Policy = "PremiumTier")]
    public class IndexModel : PageModel
    {
        public MedicalViewModel MedicalDetails { get; set; }
        public List<Cows> cowlist = new List<Cows>();

        [BindProperty]
        public int selectedCow { get; set; }

        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            if (HttpContext.Session.IsAvailable)
            {
            var id = (int)HttpContext.Session.GetInt32("Id");
            cowlist = _context.cows.Where(c => c.OwnerId == id).ToList();
            }
        }

        public void OnPostSelectCow()
        {
            if (selectedCow != 0)
            {
                MedicalDetails = (from cows in _context.cows
                                  join medic in _context.cowBreedings on cows.CowId equals medic.CowId
                                  where cows.CowId == selectedCow
                                  select new MedicalViewModel
                                  {
                                      Cowname = cows.CowName,
                                      DOB = cows.DOB,
                                      BreedingDate = medic.BreedingDate,
                                      DryDaysEnd = medic.DryDaysEnd,
                                      DryDaysStart = medic.DryDaysStart,
                                      LactationEndDate = medic.LactationEndDate,
                                      LastCalvingDate = medic.LastCalvingDate,
                                      NextCalvingDate = medic.NextCalvingDate,
                                  }).FirstOrDefault();
            }
        }

        // Get status class based on date
        public string GetDateStatus(DateTime date)
        {
            if (date < DateTime.Now)
                return "date-passed";  // Gray color for past dates
            else if (date < DateTime.Now.AddDays(7))
                return "date-nearing"; // Red color for dates nearing
            else
                return "date-normal"; // Normal date (Green)
        }

        // Get the status text
        public string GetDateStatusText(DateTime date)
        {
            if (date < DateTime.Now)
                return "Passed";  // Gray color for past dates
            else if (date < DateTime.Now.AddDays(7))
                return "Nearing"; // Red color for dates nearing
            else
                return "Normal"; // Green color for future dates
        }
    }
}
