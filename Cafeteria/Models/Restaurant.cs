using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cafeteria.Models
{
    public class Restaurant
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        public required string LocationDescription { get; set; }
        public required string ContactNumber { get; set; }

        public List<MenuItem> MenuItems { get; set; } = new();
    }
}