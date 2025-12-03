using Cafeteria.Data;
using Cafeteria.Interfaces;
using Cafeteria.Models;
using Microsoft.EntityFrameworkCore;

namespace Cafeteria.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _db;
        private const decimal Threshold = 250m;
        private const decimal BonusPerThreshold = 500m;

        public EmployeeService(ApplicationDbContext db) { _db = db; }
        public async Task<DepositResult> DepositAsync(string employeeNumber, decimal amount)
        {
            if (amount <= 0)
            {
                return new DepositResult { Success = false, Message = "Deposit amount must be positive." };
            }

            var emp = await _db.Employees.Include(e => e.Deposits).FirstOrDefaultAsync(e => e.EmployeeNumber == employeeNumber);
            if (emp == null)
            {
                return new DepositResult { Success = false, Message = "Employee not found." };
            }

            // Determine the month-year to consider
            var now = DateTime.UtcNow;
            var monthStart = new DateTime(now.Year, now.Month, 1);

            // Sum deposits in current month before this deposit
            var priorMonthTotal = emp.Deposits
                .Where(d => d.Date.Year == monthStart.Year && d.Date.Month == monthStart.Month)
                .Sum(d => d.Amount);

            var totalAfter = priorMonthTotal + amount;

            var prevThresholds = (int)Math.Floor(priorMonthTotal / Threshold);
            var newThresholds = (int)Math.Floor(totalAfter / Threshold);
            var thresholdsCrossed = Math.Max(0, newThresholds - prevThresholds);
            var bonus = thresholdsCrossed * BonusPerThreshold;

            // Add deposit record
            var deposit = new Deposit
            {
                EmployeeId = emp.Id,
                Employee = emp,
                Amount = amount,
                Date = DateTime.UtcNow
            };

            _db.Deposits.Add(deposit);

            // Update balance
            emp.Balance += amount + bonus;

            // Update LastDepositMonth (store first day of month)
            emp.LastDepositMonth = monthStart;

            await _db.SaveChangesAsync();

            return new DepositResult
            {
                Success = true,
                Message = thresholdsCrossed > 0 ? $"Applied {thresholdsCrossed}x bonus (R{bonus})." : "No bonus applied.",
                BonusApplied = bonus,
                NewBalance = emp.Balance
            };
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
            => await _db.Employees.AsNoTracking().ToListAsync();

        public async Task<Employee> GetByEmployeeNumberAsync(string employeeNumber)
        {
            return await _db.Employees.Include(e => e.Deposits).FirstOrDefaultAsync(e => e.EmployeeNumber == employeeNumber);
        }

        public async Task<Employee> GetByIdAsync(int id)
            => await _db.Employees.Include(e => e.Deposits).FirstOrDefaultAsync(e => e.Id == id);
    }
}
