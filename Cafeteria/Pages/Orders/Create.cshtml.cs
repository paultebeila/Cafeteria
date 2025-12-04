using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cafeteria.Data;
using Cafeteria.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafeteria.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public CreateModel(ApplicationDbContext db) => _db = db;

        public IList<Restaurant> Restaurants { get; set; }

        [BindProperty]
        public int SelectedRestaurantId { get; set; }

        [BindProperty]
        public Dictionary<int, int> Quantities { get; set; } = new();

        public IList<MenuItem> MenuItems { get; set; }

        public Employee CurrentEmployee { get; set; } // Assume selected/identified

        public async Task OnGetAsync()
        {
            Restaurants = await _db.Restaurants.ToListAsync();
        }

        public async Task<IActionResult> OnPostLoadMenuAsync()
        {
            Restaurants = await _db.Restaurants.ToListAsync();
            MenuItems = await _db.MenuItems.Where(m => m.RestaurantId == SelectedRestaurantId).ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostPlaceOrderAsync()
        {
            MenuItems = await _db.MenuItems.Where(m => m.RestaurantId == SelectedRestaurantId).ToListAsync();
            CurrentEmployee = await _db.Employees.FirstOrDefaultAsync(); // For demo, choose first employee

            var total = Quantities.Sum(q =>
                MenuItems.First(m => m.Id == q.Key).Price * q.Value
            );

            if (CurrentEmployee.Balance < total)
            {
                ModelState.AddModelError(string.Empty, "Insufficient balance.");
                return Page();
            }

            var order = new Order
            {
                EmployeeId = CurrentEmployee.Id,
                OrderDate = System.DateTime.UtcNow,
                TotalAmount = total,
                Status = "Pending",
                Items = Quantities.Select(q => new OrderItem
                {
                    MenuItemId = q.Key,
                    Quantity = q.Value,
                    UnitPriceAtOrder = MenuItems.First(m => m.Id == q.Key).Price
                }).ToList()
            };

            CurrentEmployee.Balance -= total;
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
