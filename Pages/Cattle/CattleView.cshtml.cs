using DairyFarm.Data.DBContext;
using DairyFarm.Data.Entity;
using DairyFarm.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks.Dataflow;

namespace DairyFarm.Pages.Cattle
{
    [Authorize(Policy ="FreeTier")]
    public class CattleViewModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public List<Cows> Cows { get; set; } = new List<Cows>();

        [BindProperty]
        public FeedLogModel  FeedLog { get; set; }


        public CattleViewModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            var ownerId = HttpContext.Session.GetInt32("Id");
            Cows = _context.cows.Where(c=>c.OwnerId==ownerId).ToList();
        }

        public async Task<IActionResult> OnGetCowDetailsAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Cow ID.");
            }
            var cowDetails = await _context.cows
            .Where(cow => cow.CowId == id)
            .Select(cow => new CowDetailsViewModel
            {
                Name = cow.CowName,
                Breed = cow.Breed,
                DOB = cow.DOB
            })
            .FirstOrDefaultAsync();

            var milkData = await _context.milkProductions
            .Where(milk => milk.CowId == id)
            .GroupBy(milk => milk.CowId)
            .Select(group => new
            {
                TotalMilkProduction = group.Sum(m => m.MilkYieldLitres),
                TotalMilkIncome = group.Sum(m => m.MilkYieldLitres*30) 
            })
            .FirstOrDefaultAsync();

            var expenseData = await _context.expenses
            .Where(exp => exp.CowId == id)
            .GroupBy(exp => exp.CowId)
            .Select(group => new
            {
                TotalExpenses = group.Sum(e => e.Amount)
            })
            .FirstOrDefaultAsync();

            if (cowDetails != null)
            {
                cowDetails.TotalMilkProduced = milkData?.TotalMilkProduction ?? 0;
                cowDetails.MilkIncome = milkData?.TotalMilkIncome ?? 0;
                cowDetails.FeedExpense = expenseData?.TotalExpenses ?? 0;
            }



            if (cowDetails == null)
            {
                return NotFound("Cow not found.");
            }

            return Partial("Partials/_CowDetails", cowDetails);
        }


        

        public IActionResult OnGetFeedForm(int cowId)
        {
            if (cowId <= 0)
            {
                return BadRequest("Invalid Cow ID.");
            }

            var foodtypes = _context.foods.Select(f => new FoodNameViewModel { Name= f.Name,PricePerKg= f.PricePerUnit }).ToList();


            var model = new FeedLogModel
            {
                CowId = cowId,
                FoodNames=foodtypes
            };

            return Partial("Partials/_FeedDetailsForm", model);
        }


        public IActionResult OnPostAddFeed()
        {
            if (!TryValidateModel(FeedLog))
            {
                return new JsonResult(new { success = false, message = "Invalid data." });
            }
            if (HttpContext.Session.GetInt32("Id") != null)
            {
            var ownerID = (int)HttpContext.Session.GetInt32("Id");

            var pricePerKg = _context.foods.Where(r => r.Name == FeedLog.FoodType).Select(r => r.PricePerUnit).FirstOrDefault();
            var totalPrice = pricePerKg * FeedLog.Quantity;
            var feeds = new FeedingLog
            {
                CowId = FeedLog.CowId,
                Date = FeedLog.FeedDate,
                FoodName = FeedLog.FoodType,
                QuantityKG = FeedLog.Quantity,
                TotalCost = totalPrice
            };
            var expensevar = new Expense
            {
                CowId = FeedLog.CowId,
                Amount = totalPrice,
                Category = "Feed",
                Date = FeedLog.FeedDate,
                OwnerId = ownerID
            };



            
            _context.feedinglogs.Add(feeds);
            _context.expenses.Add(expensevar);

            _context.SaveChanges();

            return new JsonResult(new { success = true });
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }


       

    }
}


