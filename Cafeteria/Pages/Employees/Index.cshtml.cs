using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cafeteria.Data;
using Cafeteria.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cafeteria.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IndexModel(ApplicationDbContext db) => _db = db;

        public IList<Employee> Employees { get; set; }

        public async Task OnGetAsync()
        {
            Employees = await _db.Employees.AsNoTracking().ToListAsync();
        }
    }
}
