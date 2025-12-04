using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cafeteria.Data;
using Cafeteria.Models;
using System.Threading.Tasks;

namespace Cafeteria.Pages.Restaurants
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public CreateModel(ApplicationDbContext db) => _db = db;

        [BindProperty]
        public Restaurant Restaurant { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            _db.Restaurants.Add(Restaurant);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
