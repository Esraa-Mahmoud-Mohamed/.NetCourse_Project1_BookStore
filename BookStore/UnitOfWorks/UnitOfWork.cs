using BookStore.Models;
using BookStore.Repository;
using BookStore.UnitOfWorks;

namespace BookStore.UnitOfWorks
{
    public class UnitOfWork
    {
        GenericRepository<Author> authorRepo;
        GenericRepository<Catalog> catalogRepo;
        GenericRepository<Book> bookRepo;
        GenericRepository<Order> orderRepo;
        GenericRepository<OrderDetails> orderDetailsRepo;
        
        BookStoreContext db;
        public UnitOfWork(BookStoreContext db)
        {
            this.db = db;
        }

        public GenericRepository<Author> authorGenericRepository
        { 
            get {
                if(authorRepo == null)
                    authorRepo = new GenericRepository<Author>(db);

                 return authorRepo;
            }
        }
        public GenericRepository<Catalog> catalogGenericRepository
        {
            get
            {
                if (catalogRepo == null)
                    catalogRepo = new GenericRepository<Catalog>(db);

                return catalogRepo;
            }

        }
        public GenericRepository<Book> bookGenericRepository
        {
            get
            {
                if (bookRepo == null)
                    bookRepo = new GenericRepository<Book>(db);

                return bookRepo;
            }
        }
        public GenericRepository<Order> orderGenericRepository
        {
            get
            {
                if (orderRepo == null)
                    orderRepo = new GenericRepository<Order>(db);

                return orderRepo;
            }

        }

        public void save()
        {
            db.SaveChanges();
        }
    }
}
