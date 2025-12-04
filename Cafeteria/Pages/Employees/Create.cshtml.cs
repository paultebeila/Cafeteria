using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cafeteria.Models;
using Cafeteria.Data;
using System.Threading.Tasks;

namespace Cafeteria.Pages.Employees
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public CreateModel(ApplicationDbContext db) => _db = db;

        [BindProperty]
        public Employee Employee { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            Employee.Balance = 0;
            _db.Employees.Add(Employee);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
