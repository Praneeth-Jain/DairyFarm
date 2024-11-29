using DairyFarm.Data.DBContext;
using DairyFarm.Data.Entity;
using DairyFarm.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace DairyFarm.Pages.Cattle
{
    [Authorize(Policy="PremiumTier")]
    public class CowStatsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<CowStatsView> cowStats { get; set; }=new List<CowStatsView>();

        [BindProperty(SupportsGet =true)]
        public string SearchString { get; set; }

        public string NameSort { get; set; }

        public string PurchasedSort { get; set; }

        public string BreedSort     { get; set; }

        public string BreedDateSort {  get; set; }

        public string LastCalvSort { get; set; }

        public string NextCalvSort { get; set; }

        public string LactationSort { get; set; }

        public CowStatsModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet(string? SortOrder)
        {
            var Ownerid =(int) HttpContext.Session.GetInt32("Id");
            var cows = (from cow in _context.cows
                        join cowBreed in _context.cowBreedings on cow.CowId equals cowBreed.CowId where cow.OwnerId == Ownerid
                        select new CowStatsView
                        {
                            Cowid = cowBreed.CowId,
                            BreedingDate = cowBreed.BreedingDate,
                            Breed = cow.Breed,
                            CowName = cow.CowName,
                            LactationEndDate = cowBreed.LactationEndDate,
                            PurchaseDate = cow.PurschasedDate,
                            LastCalvingDate = cowBreed.LastCalvingDate,
                            NextCalvingDate = cowBreed.NextCalvingDate
                        }).AsQueryable();
            if (SearchString != null)
            {
                cows=cows.Where(c => c.CowName.Contains(SearchString));
            }
            NameSort= SortOrder.IsNullOrEmpty() ? "name-desc" : "";
            PurchasedSort = SortOrder == "Date" ? "date_desc" : "Date";
            BreedSort = SortOrder == "Breed" ? "breed_desc" : "Breed";
            BreedDateSort = SortOrder == "BreedDate" ? "breed_date_desc" : "BreedDate";
            LactationSort = SortOrder == "Lactation" ? "lact_desc" : "Lactation";
            LastCalvSort = SortOrder == "Last" ? "last_desc" : "Last";
            NextCalvSort = SortOrder == "Next" ? "next_desc" : "Next";

            switch (SortOrder)
            {
                case "name-desc":
                    cows=cows.OrderByDescending(c => c.CowName);
                    break;
                case "Date":
                    cows = cows.OrderBy(c => c.PurchaseDate);
                    break;
                case "date_desc":
                    cows = cows.OrderByDescending(c => c.PurchaseDate);
                    break;
                case "Breed":
                    cows = cows.OrderBy(c => c.Breed);
                    break;
                case "breed_desc":
                    cows = cows.OrderByDescending(c => c.Breed);
                    break;
                case "BreedDate":
                    cows = cows.OrderBy(c => c.BreedingDate);
                    break;
                case "breed_date_desc":
                    cows=cows.OrderByDescending(c=> c.BreedingDate);
                    break;
                case "Lactation":
                    cows = cows.OrderBy(c => c.LactationEndDate);
                    break;
                case "lact_desc":
                    cows = cows.OrderByDescending(c => c.LactationEndDate);
                    break;
                case "Last":
                    cows = cows.OrderBy(c => c.LastCalvingDate);
                    break;
                case "last_desc":
                    cows = cows.OrderByDescending(c => c.LastCalvingDate);
                    break;
                case "Next":
                    cows = cows.OrderBy(c => c.NextCalvingDate);
                    break;
                case "next_desc":
                    cows = cows.OrderByDescending(c => c.NextCalvingDate);
                    break;
                default:
                    cows=cows.OrderBy(c => c.CowName);
                    break;
            }
            cowStats= cows.ToList();
        }
    }
}
