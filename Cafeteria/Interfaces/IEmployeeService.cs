using Cafeteria.Models;

namespace Cafeteria.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee> GetByEmployeeNumberAsync(string employeeNumber);
        Task<Employee> GetByIdAsync(int id);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<DepositResult> DepositAsync(string employeeNumber, decimal amount);
    }
}
