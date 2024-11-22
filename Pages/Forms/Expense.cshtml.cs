using DairyFarm.Data.DBContext;
using DairyFarm.Data.Entity;
using DairyFarm.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DairyFarm.Pages.Forms
{
    public class ExpenseModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ExpenseModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public ExpenseClass exp {  get; set; }
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

            var expense = new Expense
            {
                OwnerId = OwnerId,
                Category = exp.Category,
                Amount = exp.Amount,
                Date = exp.Date,


            };
            _context.expenses.Add(expense);
            _context.SaveChanges();
            TempData["message"] = "Expense Added Successfully";
            return Page();
        }
    }
}
