using Microsoft.EntityFrameworkCore;
using ShopApp.Model;

namespace ShopApp.DataAccess.Concrete
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-7P3FIAH;Database=ShopApp;Integrated Security=True;TrustServerCertificate=True");
            }
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>()
                    .HasKey(c => new { c.CategoryId, c.ProductId });
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}