using DairyFarm.Data.DBContext;
using DairyFarm.Data.Entity;
using DairyFarm.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DairyFarm.Pages.Cattle
{
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
            Cows = _context.cows.ToList();
        }

        public async Task<IActionResult> OnGetCowDetailsAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Cow ID.");
            }

            var cowDetails = await _context.cows
                .Where(c => c.CowId == id)
                .Select(c => new CowDetailsViewModel
                {
                    Name = c.CowName,
                    Breed = c.Breed,
                    DOB = c.DOB,
                })
                .FirstOrDefaultAsync();

            if (cowDetails == null)
            {
                return NotFound("Cow not found.");
            }

            // Use the correct path for the partial view
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

            var ownerID = (int)HttpContext.Session.GetInt32("OwnerID");

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

            // Return a JSON response indicating success
            return new JsonResult(new { success = true });
        }


       

    }
}


