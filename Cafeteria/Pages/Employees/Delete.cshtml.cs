using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cafeteria.Data;
using Cafeteria.Models;
using System.Threading.Tasks;

namespace Cafeteria.Pages.Employees
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public DeleteModel(ApplicationDbContext db) => _db = db;

        [BindProperty]
        public Employee Employee { get; set; }

        public async Task OnGetAsync(int id)
        {
            Employee = await _db.Employees.FindAsync(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var emp = await _db.Employees.FindAsync(Employee.Id);
            if (emp != null)
            {
                _db.Employees.Remove(emp);
                await _db.SaveChangesAsync();
            }
            return RedirectToPage("Index");
        }
    }
}
