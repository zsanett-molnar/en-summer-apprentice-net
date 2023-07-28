using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TMS.Models;

namespace TMS.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TicketManagementDbContext _dbContext;

        public OrderRepository()
        {
            _dbContext = new TicketManagementDbContext();
        }

        public int Add(Order @order)
        {
            _dbContext.Add(@order);
            _dbContext.SaveChanges();
            return @order.OrderId;
        }


        public void Delete(Order order)
        {
            _dbContext.Remove(@order);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Order> GetAll()
        {
            var orders = _dbContext.Orders;

            return orders;
        }

        public async Task<Order> GetById(int id)
        {
            var @order = await _dbContext.Orders.Where(o => o.OrderId == id).FirstOrDefaultAsync();

            return @order;
        }

        public void Update(Order order)
        {
            _dbContext.Entry(@order).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
