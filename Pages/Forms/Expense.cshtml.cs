using DairyFarm.Data.DBContext;
using DairyFarm.Data.Entity;
using DairyFarm.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DairyFarm.Pages.Forms
{
    [Authorize(Policy = "StandardTier")]
    public class ExpenseModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ExpenseModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public ExpenseClass exp {  get; set; }

        public int? CowId { get; set; }
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

            if (exp.CowName != null)
            {
                CowId=_context.cows.Where(c=>c.CowName==exp.CowName).Select(c=>c.CowId).FirstOrDefault();
            }
            else
            {
                CowId = null;
            }

            var expense = new Expense
            {
                OwnerId = OwnerId,
                Category = exp.Category,
                Amount = exp.Amount,
                Date = exp.Date,
                CowId=CowId
            };
            _context.expenses.Add(expense);
            _context.SaveChanges();
            TempData["Message"] = "Expense Added Successfully";
            return RedirectToPage();
        }
    }
}
