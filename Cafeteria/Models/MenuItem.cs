using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafeteria.Models
{
    public class MenuItem
    {
        public int Id { get; set; }

        [Required]
        public int RestaurantId { get; set; }
        public required Restaurant Restaurant { get; set; }

        [Required]
        public required string Name { get; set; }
        public required string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}