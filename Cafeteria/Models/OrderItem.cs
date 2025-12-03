using System.ComponentModel.DataAnnotations.Schema;

namespace Cafeteria.Models
{    
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }

        public int Quantity { get; set; }

        // price captured at time of order
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPriceAtOrder { get; set; }
    }
}
