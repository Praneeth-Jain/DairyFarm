using DairyFarm.Data.DBContext;
using DairyFarm.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

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
                var tierUser=_context.subscriptions.Where(o=>o.OwnerId==getOwner.OwnerId).FirstOrDefault();
                var tier = tierUser.Tier;

                var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, getOwner.OwnerId.ToString()),
        new Claim("SubscriptionType", tier) 
    };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = false
                };

               HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                HttpContext.Session.SetInt32("Id", getOwner.OwnerId);

                return RedirectToPage("/Cattle/CattleView");

            }
            else
            {
                return Page();
            }
        }
    }
}
