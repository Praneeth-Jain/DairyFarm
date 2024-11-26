using DairyFarm.Data.DBContext;
using DairyFarm.Data.Entity;
using DairyFarm.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DairyFarm.Pages.Forms
{
    [Authorize(Policy = "FreeTier")]
    public class FoodModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public FoodModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public FoodClass food { get; set; }

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

            var foods = new Food
            {
                OwnerId = OwnerId,
                Name = food.Name,
                PricePerUnit = food.PricePerUnit,
            };
            _context.foods.Add(foods);
            _context.SaveChanges();
            TempData["Message"] = "Foods Added Successfully";
            return RedirectToPage();
        }
    }
}
