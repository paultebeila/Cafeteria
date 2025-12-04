using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cafeteria.Data;
using Cafeteria.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cafeteria.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IndexModel(ApplicationDbContext db) => _db = db;

        public IList<Order> Orders { get; set; }

        public async Task OnGetAsync()
        {
            Orders = await _db.Orders
                .Include(o => o.Employee)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }
    }
}
