using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateOnly PublishDate { get; set; }
        [ForeignKey("Catalog")]
        public int cat_id { get; set; }
        public virtual Catalog Catalog { get; set; }
        [ForeignKey("Author")]
        public int auth_id { get; set; }
        public virtual Author Author { get; set; }
    }
}
