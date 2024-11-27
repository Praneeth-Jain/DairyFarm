using DairyFarm.Data.DBContext;
using DairyFarm.Data.Entity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DairyFarm.Pages.Subscription
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public string CurrentSubscription { get; set; } = "free"; 

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            var subscriptionClaim = User.FindFirst("SubscriptionType");
            if (subscriptionClaim != null)
            {
                CurrentSubscription = subscriptionClaim.Value.ToLower();
            }
        }

        
        public IActionResult OnPostSubscribe(string selectedTier)
        {
            if (selectedTier != null)
            {
                var userId = (int)HttpContext.Session.GetInt32("Id");
                var today = DateTime.Now;
                var endday = today.AddYears(1);
                var status = "Active";



                var Users = _context.subscriptions.Where(s => s.OwnerId == userId).FirstOrDefault();
                if (Users != null)
                {
                    var existing_tier = Users.Tier;
                    var validation =
                    (existing_tier == "free" && (selectedTier == "standard" || selectedTier == "premium")) ||
                    (existing_tier == "standard" && selectedTier == "premium");
                    if (!validation)
                    {
                        TempData["SubMessage"] = "Subscription is already active till   " + Users.EndDate.Date.ToString("d");
                        return Page();
                    }
                    Users.StartDate = today;
                    Users.EndDate = endday;
                    Users.Status = status;
                    Users.Tier = selectedTier;
                    var identity = User.Identity as ClaimsIdentity;
                    if (identity != null)
                    {
                        var subscriptionClaim = identity.FindFirst("SubscriptionType");
                        if (subscriptionClaim != null)
                        {
                            identity.RemoveClaim(subscriptionClaim);
                        }

                        identity.AddClaim(new Claim("SubscriptionType", selectedTier));

                        HttpContext.SignOutAsync();
                        HttpContext.SignInAsync(new ClaimsPrincipal(identity));
                        _context.SaveChanges();
                        return RedirectToPage("/Index");

                    }



                }
            }

                return Page();
        }
    }
}
