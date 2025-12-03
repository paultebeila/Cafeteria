using Cafeteria.Data;
using Cafeteria.Interfaces;
using Cafeteria.Models;
using Microsoft.EntityFrameworkCore;

namespace Cafeteria.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _db;

        public OrderService(ApplicationDbContext db) { _db = db; }

        public async Task<(bool Success, string Message, Order Order)> PlaceOrderAsync(string employeeNumber, IDictionary<int, int> menuItemIdToQty)
        {
            if (menuItemIdToQty == null || !menuItemIdToQty.Any())
                return (false, "Order must contain at least one item.", null);

            var emp = await _db.Employees.FirstOrDefaultAsync(e => e.EmployeeNumber == employeeNumber);
            if (emp == null) return (false, "Employee not found.", null);

            // load items and compute price
            var menuItemIds = menuItemIdToQty.Keys.ToList();
            var items = await _db.MenuItems.Where(mi => menuItemIds.Contains(mi.Id)).ToListAsync();

            if (items.Count != menuItemIds.Count)
                return (false, "One or more menu items are invalid.", null);

            decimal total = 0m;
            var order = new Order
            {
                EmployeeId = emp.Id,
                Employee = emp,
                OrderDate = DateTime.UtcNow,
                Status = "Pending",
                Items = new List<OrderItem>()
            };

            foreach (var mi in items)
            {
                var qty = menuItemIdToQty[mi.Id];
                if (qty <= 0) return (false, "Invalid quantity.", null);

                var unitPrice = mi.Price;
                total += unitPrice * qty;

                order.Items.Add(new OrderItem
                {
                    MenuItemId = mi.Id,
                    Quantity = qty,
                    UnitPriceAtOrder = unitPrice
                });
            }

            if (emp.Balance < total)
            {
                return (false, "Insufficient balance.", null);
            }

            // Deduct balance
            emp.Balance -= total;
            order.TotalAmount = total;

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            // include navigation properties if needed
            return (true, "Order placed.", order);
        }

        /////////////////
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _db.Orders.Include(o => o.Employee).Include(o => o.Items).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersForEmployeeAsync(int employeeId)
        {
            return await _db.Orders
            .Include(o => o.Items).ThenInclude(oi => oi.MenuItem)
            .Where(o => o.EmployeeId == employeeId)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, string newStatus)
        {
            var order = await _db.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            {
                return false;
            }
            order.Status = newStatus;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
