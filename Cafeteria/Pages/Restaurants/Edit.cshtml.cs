using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cafeteria.Data;
using Cafeteria.Models;
using System.Threading.Tasks;

namespace Cafeteria.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public EditModel(ApplicationDbContext db) => _db = db;

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
            if (!ModelState.IsValid) return Page();

            _db.Attach(Restaurant).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
