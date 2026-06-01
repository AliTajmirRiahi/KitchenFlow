using Arta.Domain.Core.Commons.Enums;
using Arta.Domain.Core.Model;
using Microsoft.EntityFrameworkCore;
using KitchenFlow.Application;
using KitchenFlow.Domain.Order;
using KitchenFlow.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenFlow.Infrastructure.Repositories
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

        public async Task DeleteAsync(Order order)
        {
            order.EnsureCanBeDeleted();

            _dbContext.Orders.Remove(order);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            order.Validate();

            _dbContext.Orders.Update(order);

            await _dbContext.SaveChangesAsync();
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

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Orders
                                    .Include(p => p.Items)
                                    .FirstOrDefaultAsync(o => o.Id == id);
        }

        public Task<IReadOnlyList<Order>> GetByStatusAsync(Enums.OrderStatus status)
        {
            throw new NotImplementedException();
        }

    }
}
