namespace Identity.Models.ViewModels
{
    public class CustomersListViewModel
    {
        public IQueryable<Customer> Customers { get; set; }
        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();
    }
}
