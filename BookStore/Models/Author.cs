namespace BookStore.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Bio { get; set; }
        public int NoOfBooks { get; set; }

        public virtual List<Book> Books { get; set; } = new List<Book>();
    }
}
