using DairyFarm.Data.DBContext;
using DairyFarm.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DairyFarm.Pages
{
    [Authorize(Policy="FreeTier")]
    public class OwnerProfileModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public Owners owners {  get; set; }

        public string Tier {  get; set; }

        public OwnerProfileModel(ApplicationDbContext context) 
        {
            _context = context;
        }
        public void OnGet()
        {
            var currentOwner =(int) HttpContext.Session.GetInt32("Id");
            owners = _context.owners.Where(o => o.OwnerId == currentOwner).FirstOrDefault();
            if (User.HasClaim(c=>c.Type=="SubscriptionType")){
                Tier = User.FindFirst("SubscriptionType").Value;
            }
        }
    }
}
