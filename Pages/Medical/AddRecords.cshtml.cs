using DairyFarm.Data.DBContext;
using DairyFarm.Data.Entity;
using DairyFarm.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace DairyFarm.Pages.Medical
{
    public class AddRecordsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AddRecordsModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public AddMedicalRecordClass add {  get; set; }

        public List<Cows> CattleList { get; set; } = new();

        public void OnGet()
        {

            var Role = HttpContext.Session.GetString("UserRole");
            if (Role != "Owner")
            {
                Response.Redirect("/OwnerLogin");
                return;
            }
            CattleList = _context.cows.ToList();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                CattleList = _context.cows.ToList();
                return Page();
            }
            var OwnerId = (int)HttpContext.Session.GetInt32("Id");

            DateTime BreedingDate = add.BreedingDate ?? add.LastCalvingDate.AddMonths(9);

            var input = new CowBreeding
            {
                CowId = add.CowId,
                LastCalvingDate = add.LastCalvingDate,
                BreedingDate = BreedingDate,
                LactationEndDate = add.LastCalvingDate.AddMonths(10),
                DryDaysStart = add.LastCalvingDate.AddMonths(10),
                DryDaysEnd = add.LastCalvingDate.AddMonths(11),
                NextCalvingDate = add.LastCalvingDate.AddDays(12),
            };
            _context.cowBreedings.Add(input);
            _context.SaveChanges();
            TempData["message"] = "Medical Records Added Successfully";
            return Page();
        }
    }
}
