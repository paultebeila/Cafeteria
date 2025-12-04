using Cafeteria.Data;
using Cafeteria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Cafeteria.Pages.Employees
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public DetailsModel(ApplicationDbContext db) => _db = db;

        public Employee Employee { get; set; }

        public async Task OnGetAsync(int id)
        {
            Employee = await _db.Employees
                .Include(e => e.Deposits)
                .Include(e => e.Orders)
                .ThenInclude(o => o.Items)
                .ThenInclude(i => i.MenuItem)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
