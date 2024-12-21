using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace BookStore.Models
{
    public class BookStoreContext:IdentityDbContext
    {
        public BookStoreContext()
        {
            
        }

        public BookStoreContext(DbContextOptions<BookStoreContext> option) : base(option)
        {
            
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<OrderDetails>().HasKey("order_id", "book_id");

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id="1", Name = "admin", NormalizedName = "ADMIN" },
                new IdentityRole() { Id="2", Name = "customer", NormalizedName = "CUSTOMER" }
                );
        }
    }
}
