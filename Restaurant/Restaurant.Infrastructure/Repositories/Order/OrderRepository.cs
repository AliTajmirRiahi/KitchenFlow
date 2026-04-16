using Arta.Domain.Core.Commons.Enums;
using Arta.Domain.Core.Model;
using Restaurant.Application;
using Restaurant.Domain.Order;
using Restaurant.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Infrastructure.Repositories
{
    public class OrderRepository : ReporsitoryBase, IOrderRepository
    {
        private readonly RestaurantDbContext _dbContext;

        public OrderRepository(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Guid> AddAsync(Order order)
        {
            order.Validate();

            await _dbContext.Orders.AddAsync(order);

            await _dbContext.SaveChangesAsync();

            return order.Id;
        }

        public Task DeleteAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetActiveOrdersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetByCustomerIdAsync(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetByDateAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<Order?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetByStatusAsync(Enums.OrderStatus status)
        {
            throw new NotImplementedException();
        }

    }
}
