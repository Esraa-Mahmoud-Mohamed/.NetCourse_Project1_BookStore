using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateOnly OrderDate { get; set; }
        [Column(TypeName = "money")]
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        [ForeignKey("Customer")]
        public string cust_id { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
    }
}
