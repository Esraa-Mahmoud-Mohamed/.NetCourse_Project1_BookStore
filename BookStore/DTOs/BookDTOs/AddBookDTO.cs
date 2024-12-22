namespace BookStore.DTOs.BookDTOs
{
    public class AddBookDTO
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateOnly PublishDate { get; set; }
        public int? cat_id { get; set; }
        public int? auth_id { get; set; }
    }
}
