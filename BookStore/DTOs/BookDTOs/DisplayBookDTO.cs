using BookStore.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.DTOs.BookDTOs
{
    public class DisplayBookDTO
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateOnly PublishDate { get; set; }
        public string CatalogName { get; set; }
        public string AuthorName { get; set; }
    }
}
