using DairyFarm.Data.DBContext;
using DairyFarm.Data.Entity;
using Microsoft.AspNetCore.Mvc;

namespace DairyFarm.ViewComponents
{

    using Microsoft.AspNetCore.Mvc;

    public class MenuViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public MenuViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var menuItems = _context.menuItems
                .OrderBy(m => m.Order)
                .ToList();

            var subMenuItems = _context.subMenuItems
                .OrderBy(s => s.MenuItemId)
                .ThenBy(s => s.Order)
                .ToList();

            var model = (menuItems, subMenuItems);

            return View(model);
        }

    }
    
       
    }

