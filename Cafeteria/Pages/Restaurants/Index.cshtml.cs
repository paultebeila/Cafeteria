using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cafeteria.Data;
using Cafeteria.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cafeteria.Pages.Restaurants
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IndexModel(ApplicationDbContext db) => _db = db;

        public IList<Restaurant> Restaurants { get; set; }

        public async Task OnGetAsync()
        {
            Restaurants = await _db.Restaurants.AsNoTracking().ToListAsync();
        }
    }
}
