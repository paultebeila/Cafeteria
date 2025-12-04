using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cafeteria.Data;
using Cafeteria.Models;
using System.Threading.Tasks;

namespace Cafeteria.Pages.Restaurants
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public DeleteModel(ApplicationDbContext db) => _db = db;

        [BindProperty]
        public Restaurant Restaurant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Restaurant = await _db.Restaurants.FindAsync(id);
            if (Restaurant == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var rest = await _db.Restaurants.FindAsync(Restaurant.Id);
            if (rest != null)
            {
                _db.Restaurants.Remove(rest);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
