using DairyFarm.Data.DBContext;
using DairyFarm.Data.Entity;
using DairyFarm.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DairyFarm.Pages.Forms
{
    public class AddByProductModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        [BindProperty]
        public AddByProductsView newProduct {  get; set; }

        public AddByProductModel(ApplicationDbContext context)
        {
            _context = context;
        }


        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) {
                return Page();
            }
            var ownerId =(int) HttpContext.Session.GetInt32("Id");
            var byprod = new ByProduct
            {
                OwnerId = ownerId,
                Revenue = newProduct.Price,
                Type = newProduct.ProductName
            };

            _context.byProduct.Add(byprod);

            _context.SaveChanges();

            return RedirectToPage("/Forms/Byproduct");
        }
    }
}
