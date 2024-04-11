namespace Identity.Models
{
    public interface IProductRepository
    {
        public IQueryable<Product> Products { get; }
        public IQueryable<Order> Orders { get; }
        public IQueryable<Customer> Customers { get; }

        public IQueryable<LineItem> LineItems { get; }       
        public IQueryable<Category> Categories { get; }
        public IQueryable<ProductCategory> ProductCategories { get; }
        public IQueryable<ProductRecommendation> ProductRecommendations { get; }

        public IQueryable<ContentRecommendation> ContentRecommendations { get; }


    }
}
