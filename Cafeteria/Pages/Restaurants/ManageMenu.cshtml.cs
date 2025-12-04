using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cafeteria.Data;
using Cafeteria.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cafeteria.Pages.Restaurants
{
    public class ManageMenuModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public ManageMenuModel(ApplicationDbContext db) => _db = db;

        public Restaurant Restaurant { get; set; }
        public IList<MenuItem> MenuItems { get; set; }

        [BindProperty]
        public MenuItem NewMenuItem { get; set; }

        public async Task OnGetAsync(int id)
        {
            Restaurant = await _db.Restaurants.FindAsync(id);
            MenuItems = await _db.MenuItems
                .Where(m => m.RestaurantId == id)
                .ToListAsync();
        }
        public async Task<IActionResult> OnPostAddMenuItemAsync(int id)
        {
            if (!ModelState.IsValid) return Page();

            NewMenuItem.RestaurantId = id;
            _db.MenuItems.Add(NewMenuItem);
            await _db.SaveChangesAsync();
            return RedirectToPage(new { id });
        }

        public async Task<IActionResult> OnPostDeleteMenuItemAsync(int id, int menuItemId)
        {
            var item = await _db.MenuItems.FindAsync(menuItemId);
            if (item != null) _db.MenuItems.Remove(item);
            await _db.SaveChangesAsync();
            return RedirectToPage(new { id });
        }
    }
}
