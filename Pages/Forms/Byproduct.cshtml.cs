using DairyFarm.Data.DBContext;
using DairyFarm.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DairyFarm.Data.Entity;
using Microsoft.AspNetCore.Authorization;

namespace DairyFarm.Pages.Forms
{
    [Authorize(Policy = "StandardTier")]
    public class ByproductModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        public ByproductModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]

        public ByproductClass byprod {  get; set; }

        public List<ByProduct> byProducts { get; set; } = new List<ByProduct>();
        public void OnGet()
        {
            var OwnerId = (int)HttpContext.Session.GetInt32("Id");
            byProducts =_context.byProduct.Where(p=>p.OwnerId==OwnerId).ToList();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var OwnerId = (int)HttpContext.Session.GetInt32("Id");


            if (byprod != null) { 
                var price=_context.byProduct.Where(p=>p.Type==byprod.Type).Select(p=>p.Revenue).FirstOrDefault();
                var amt = price * byprod.Quantity;
                var income = new Income
                {
                    OwnerId = OwnerId,
                    Amount = amt,
                    Date = byprod.Date,
                    Category = "ByProduct",
                };
                _context.incomes.Add(income);
                _context.SaveChanges();
                TempData["Message"] = "ByProduct Added Successfully";
            }
            return RedirectToPage();
        }
    }
}
