using DairyFarm.Data.DBContext;
using DairyFarm.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DairyFarm.Pages.Finance
{


    public class ExpenseModel : PageModel
    {
        public IQueryable<Expense> ExpenseUser { get; set; }
        public string filterQuery { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal FoodExpense {  get; set; }

        public decimal OtherExpenses { get; set; }

        private readonly ApplicationDbContext _context;
        public ExpenseModel(ApplicationDbContext context) {
            _context = context;
        }

        public void OnGet()
        {
            var ownerID =(int) HttpContext.Session.GetInt32("Id");
            filterQuery = Request.Query["filterQuery"];

                ExpenseUser = _context.expenses.Where(e => e.OwnerId == ownerID);
            if (!string.IsNullOrEmpty(filterQuery))
            {
                if (filterQuery == "Y")
                {
                    ExpenseUser = ExpenseUser.Where(e=> e.Date.Year == DateTime.Now.Year);
                }
                else if (filterQuery == "M")
                {
                    ExpenseUser = ExpenseUser.Where(e=>e.Date.Month == DateTime.Now.Month);
                }
                else if (filterQuery == "T")
                {
                    ExpenseUser = ExpenseUser.Where(e =>  e.Date.Date == DateTime.Now.Date);
                }
            }

            var user = ExpenseUser.Where(e=>e.Category=="Feed");
            FoodExpense = user.Sum(s => s.Amount);

            TotalExpense=ExpenseUser.Sum(s => s.Amount);

            var otherexpenseuser=ExpenseUser.Where(e=>e.Category!="Feed");
            OtherExpenses=otherexpenseuser.Sum(s => s.Amount);
        }
    }
}
