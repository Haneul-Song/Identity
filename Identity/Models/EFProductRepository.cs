
namespace Identity.Models
{
    public class EFProductRepository : IProductRepository
    {
        private ProductContext _context;

        public EFProductRepository(ProductContext temp) { 
            _context = temp;
        }
        public IQueryable<Product> Products => _context.Products;
        public IQueryable<Order> Orders => _context.Orders;
        public IQueryable<Customer> Customers => _context.Customers;
        public IQueryable<LineItem> LineItems => _context.LineItems;
        public IQueryable<Category> Categories => _context.Categories;
        public IQueryable<ProductCategory> ProductCategories => _context.ProductCategories;
        public IQueryable<ProductRecommendation> ProductRecommendations => _context.ProductRecommendations;
        public IQueryable<ContentRecommendation> ContentRecommendations => _context.ContentRecommendations;


    }
}
