using Cafeteria.Models;
using System;

namespace Cafeteria.Models
{
    public class Deposit
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public required Employee Employee { get; set; }

        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
