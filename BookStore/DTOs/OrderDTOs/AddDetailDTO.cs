using BookStore.Models;

namespace BookStore.DTOs.OrderDTOs
{
    public class AddDetailDTO
    {
        public int book_id { get; set; }
        public int Quantity { get; set; }
    }
}
