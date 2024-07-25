using Microsoft.EntityFrameworkCore;
using SampleAPI.Controllers;
using SampleAPI.Entities;
using SampleAPI.Requests;

namespace SampleAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SampleApiDbContext _ctx;

        public OrderRepository(SampleApiDbContext ctx)
        {

            _ctx = ctx;

        }
        public async Task<Guid> AddNewOrder(Order order)
        {
            order.Id = Guid.NewGuid();
            await _ctx.Orders.AddAsync(order);
            return await _ctx.SaveChangesAsync() > 0 ? order.Id : Guid.Empty;
        }

        public async Task<List<Order>> GetAll()
        {
            return await _ctx.Orders.OrderByDescending(item => item.Date).ToListAsync();
        }

        public  async Task<Order> GetById(Guid id)
        {
         return await _ctx.Orders.FirstOrDefaultAsync(x  => x.Id == id);
            
        }
    }
}
