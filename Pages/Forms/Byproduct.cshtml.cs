using DairyFarm.Data.DBContext;
using DairyFarm.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DairyFarm.Data.Entity;

namespace DairyFarm.Pages.Forms
{
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
            var Role = HttpContext.Session.GetString("UserRole");
            if (Role != "Owner")
            {
                Response.Redirect("/OwnerLogin");
                return;
            }

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
