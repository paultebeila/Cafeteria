using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cafeteria.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public required string EmployeeNumber { get; set; } // unique

        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }

        // store month as first day of month to normalize
        public DateTime? LastDepositMonth { get; set; }

        public List<Deposit> Deposits { get; set; } = new();
        public List<Order> Orders { get; set; } = new();
    }
}
