using DairyFarm.Data.DBContext;
using DairyFarm.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DairyFarm.Pages.Finance
{

    [Authorize(Policy = "PremiumTier")]
    public class IncomeModel : PageModel
    {
        public decimal TotalIncome { get; set; }
        public decimal ByProductIncome { get; set; }
        public decimal MilkIncome { get; set; }

        public string filteredQuery { get; set; }

        public IQueryable<Income> IncomeUser { get; set; }

        private readonly ApplicationDbContext _context;
        public IncomeModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            var ownerID = HttpContext.Session.GetInt32("Id");
            IncomeUser = _context.incomes.Where(i => i.OwnerId == ownerID);


            filteredQuery = Request.Query["filteredQuery"];

            if (!string.IsNullOrEmpty(filteredQuery))
            {
                if (filteredQuery == "Y")
                {
                    IncomeUser = IncomeUser.Where(i => i.Date.Year == DateTime.Now.Year);
                }
                else if (filteredQuery == "M")
                {
                    IncomeUser = IncomeUser.Where(i => i.Date.Month == DateTime.Now.Month);
                }
                else if (filteredQuery == "T")
                {
                    IncomeUser = IncomeUser.Where(I => I.Date.Date == DateTime.Now.Date);
                }


            }
                TotalIncome = IncomeUser.Sum(i => i.Amount);

                MilkIncome = IncomeUser.Where(i => i.Category == "MilkProduction").Sum(i => i.Amount);

                ByProductIncome = IncomeUser.Where(i => i.Category == "ByProduct").Sum(i => i.Amount);


        }
    }
}
