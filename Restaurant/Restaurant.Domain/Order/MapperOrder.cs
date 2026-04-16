using Arta.Domain.Core.Commons;
using Restaurant.Domain.Contract.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Riok.Mapperly.Abstractions;
using System.Runtime.ConstrainedExecution;

namespace Restaurant.Domain.Order
{
    public interface IMapperOrder : IMapper
    {
        public Order OrderDtoToOrder(OrderDto dto);
        public OrderDto OrderToOrderDto(Order order);
        public Order CustomMapOrderDtoToOrder(OrderDto orderDto);
        public OrderDto CustomMapOrderToOrderDto(Order order);
    }
}
