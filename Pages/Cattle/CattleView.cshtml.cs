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

            var model = new FeedLogModel
            {
                CowId = cowId
            };

            return Partial("Partials/_FeedDetailsForm", model);
        }


        public IActionResult OnPostAddFeed()
        {
            if (!TryValidateModel(FeedLog))
            {
                return new JsonResult(new { success = false, message = "Invalid data." });
            }

            var feeds = new FeedingLog
            {
                CowId = FeedLog.CowId,
                Date = FeedLog.FeedDate,
                FoodName = FeedLog.FoodType,
                QuantityKG = FeedLog.Quantity,
                TotalCost = 500
            };

            
            _context.feedinglogs.Add(feeds);
            _context.SaveChanges();

            // Return a JSON response indicating success
            return new JsonResult(new { success = true });
        }


        public IActionResult OnPostSubmitMilk([FromBody] MilkProductionModel model)
        {
            Console.WriteLine("HEllo ee");
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { success = false, message = "Invalid milk production data." });
            }

            var milkProduction = new MilkProduction
            {
                CowId = model.CowId,
                Date = model.ProductionDate,
                MilkYieldLitres = model.Quantity
            };

            // Add the new milk production record to the database
            _context.milkProductions.Add(milkProduction);

            
            
                // Save the changes to the database
                _context.SaveChangesAsync();
            
            

            // Return a JsonResult indicating success
            return new JsonResult(new { success = true, message = "Milk production record added successfully!" });
        }


    }
}


