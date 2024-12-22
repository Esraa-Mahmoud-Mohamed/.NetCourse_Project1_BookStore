using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.DTOs.OrderDTOs
{
    public class DisplayOrderDTO
    {
        public DateOnly OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public string cust_name { get; set; }
    }
}
