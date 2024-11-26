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
        public void OnGet()
        {
          

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var OwnerId = (int)HttpContext.Session.GetInt32("Id");
            
            var byproduct = new ByProduct
            {
                OwnerId = OwnerId,
                Date = byprod.Date,
                Type = byprod.Type,
                Quantity = byprod.Quantity,
                Revenue = byprod.Revenue,

            };

            var income = new Income
            {
                OwnerId = OwnerId,
                Amount = byprod.Revenue,
                Date = byprod.Date,
                Category = "ByProduct",
            };
            _context.incomes.Add(income);
            _context.byProduct.Add(byproduct);
            _context.SaveChanges();
            TempData["Message"] = "ByProduct Added Successfully";
            return RedirectToPage();
        }
    }
}
