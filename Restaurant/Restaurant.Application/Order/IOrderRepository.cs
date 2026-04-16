using Arta.Domain.Core.Model;
using Restaurant.Domain.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Arta.Domain.Core.Commons.Enums.Enums;

namespace Restaurant.Application
{
    public interface IOrderRepository : IRepositoryBase
    {
        Task<Order?> GetByIdAsync(Guid id);
        Task<IReadOnlyList<Order>> GetByCustomerIdAsync(int customerId);
        Task<IReadOnlyList<Order>> GetByStatusAsync(OrderStatus status);
        Task<IReadOnlyList<Order>> GetActiveOrdersAsync();
        Task<IReadOnlyList<Order>> GetByDateAsync(DateTime date);

        Task<Guid> AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Order order);
    }
}
