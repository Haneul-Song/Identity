using Microsoft.EntityFrameworkCore;

namespace Identity.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }
        public DbSet<Product> Products {  get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductRecommendation> ProductRecommendations { get; set; }

        public DbSet<ContentRecommendation> ContentRecommendations { get; set; }




    }
}
