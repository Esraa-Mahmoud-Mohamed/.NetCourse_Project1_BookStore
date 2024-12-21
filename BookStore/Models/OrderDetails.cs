using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class OrderDetails
    {
        [ForeignKey("Order")]
        public int order_id { get; set; }
        public virtual Order Order { get; set; }
        [ForeignKey("Book")]
        public int book_id { get; set; }
        public virtual Book Book { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }
    }
}
