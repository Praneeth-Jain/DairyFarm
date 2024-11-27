using DairyFarm.Data.DBContext;
using DairyFarm.Data.Entity;
using DairyFarm.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DairyFarm.Pages.Cattle
{

    [Authorize(Policy ="FreeTier")]
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
            

        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            var OwnerId = (int)HttpContext.Session.GetInt32("Id");

            var cowCount=_context.cows.Count(x=>x.OwnerId == OwnerId);
            var cowlimit = 0;
            var tier = User.FindFirst("SubscriptionType").Value;
            if (tier != null)
            {
                if (tier == "free")
                {
                    cowlimit = 5;
                }
                else if(tier == "standard")
                {
                    cowlimit = 20;
                }
                else
                {
                    cowlimit=int.MaxValue;
                }
                if (cowCount >= cowlimit) {
                    
                    return RedirectToPage("/Subscription/Index");
                }
            }
            
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
