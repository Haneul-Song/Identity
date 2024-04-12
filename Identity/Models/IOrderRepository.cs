using Identity.Models;

namespace Identity.Models
{

    public interface IOrderRepository
    {

        IQueryable<Order> Orders { get; }
        void SaveOrder(Order order);
    }
}
