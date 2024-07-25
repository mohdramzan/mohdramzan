using SampleAPI.Entities;
using SampleAPI.Requests;

namespace SampleAPI.Repositories
{
    public interface IOrderRepository
    {
        // TODO: Create repository methods.

        // Suggestions for repo methods:
        // public GetRecentOrders();
        // public AddNewOrder();


        Task<List<Order>> GetAll();
        Task<Guid> AddNewOrder(Order order);
        Task<Order> GetById(Guid id);

    }
}
