using Arta.Application.Core;
using Restaurant.Domain.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application
{
    public class OrderService : ApplicationService, IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Guid> AddAsync(Order order)
        {
            return await _orderRepository.AddAsync(order);
        }
    }
}
