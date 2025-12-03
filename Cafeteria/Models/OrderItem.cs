using System.ComponentModel.DataAnnotations.Schema;

namespace Cafeteria.Models
{    
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public required Order Order { get; set; }

        public int MenuItemId { get; set; }
        public required MenuItem MenuItem { get; set; }

        public int Quantity { get; set; }

        // price captured at time of order
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPriceAtOrder { get; set; }
    }
}
