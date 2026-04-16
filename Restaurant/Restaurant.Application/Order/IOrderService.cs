using Restaurant.Domain.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application
{
    public interface IOrderService 
    {
        public Task<Guid> AddAsync(Order order);
    }
}
