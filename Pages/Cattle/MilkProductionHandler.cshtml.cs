using DairyFarm.Data.DBContext;
using DairyFarm.Data.Entity;
using DairyFarm.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DairyFarm.Pages.Cattle
{
    public class MilkProductionHandlerModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public MilkProductionModel MilkProductionMod { get; set; }

        public MilkProductionHandlerModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }
        public IActionResult OnGetMilkProductionForm(int cowId)
        {
            var model = new MilkProductionModel { CowId = cowId };
            return Partial("Partials/_MilkProductionForm", model);
        }

        public IActionResult OnPostSubmitMilk()
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { success = false, message = "Invalid milk production data." });
            }

            var milkProduction = new MilkProduction
            {
                CowId = MilkProductionMod.CowId,
                Date = MilkProductionMod.ProductionDate,
                MilkYieldLitres = MilkProductionMod.Quantity
            };

            var ownerID =(int) HttpContext.Session.GetInt32("OwnerID");

            var milkprice = 30;

            var incomevar = new Income
            {
                CowId = MilkProductionMod.CowId,
                Date = MilkProductionMod.ProductionDate,
                Amount = milkprice * MilkProductionMod.Quantity,
                Category = "MilkProduction",
                OwnerId = ownerID,
            };

            _context.milkProductions.Add(milkProduction);
            _context.incomes.Add(incomevar);
            _context.SaveChanges();

            TempData["MilkMsg"] = "Milk Production Record Added Succesfully";

            return new JsonResult(new { success = true, message = "Milk production record added successfully!" });
        }
    }
}
