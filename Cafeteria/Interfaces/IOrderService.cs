using Cafeteria.Models;

namespace Cafeteria.Interfaces
{
    public interface IOrderService
    {
        Task<(bool Success, string Message, Order Order)> PlaceOrderAsync(string employeeNumber, IDictionary<int, int> menuItemIdToQty);
        Task<IEnumerable<Order>> GetOrdersForEmployeeAsync(int employeeId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<bool> UpdateOrderStatusAsync(int orderId, string newStatus);
    }
}
