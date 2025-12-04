using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cafeteria.Data;
using Cafeteria.Models;
using System.Threading.Tasks;

namespace Cafeteria.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public EditModel(ApplicationDbContext db) => _db = db;

        [BindProperty]
        public Employee Employee { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Employee = await _db.Employees.FindAsync(id);
            if (Employee == null) return RedirectToPage("Index");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var emp = await _db.Employees.FindAsync(Employee.Id);
            if (emp == null) return RedirectToPage("Index");

            emp.Name = Employee.Name;
            emp.EmployeeNumber = Employee.EmployeeNumber;
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
