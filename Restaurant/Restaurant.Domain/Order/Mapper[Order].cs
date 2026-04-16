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
    [Mapper]
    public partial class Mapper : IMapperOrder
    {
        public partial Order OrderDtoToOrder(OrderDto dto);
        public partial OrderDto OrderToOrderDto(Order dto);

        [UserMapping(Default = true)]
        public Order CustomMapOrderDtoToOrder(OrderDto orderDto)
        {
            // custom before map code...
            var order = OrderDtoToOrder(orderDto);
            // custom after map code...
            foreach (var item in orderDto.Items)
            {
                order.AddItem(item.ProductId, item.Quantity, item.UnitPrice);
            }
            return order;
        }
        [UserMapping(Default = true)]
        public OrderDto CustomMapOrderToOrderDto(Order order)
        {
            // custom before map code...
            var orderDto = OrderToOrderDto(order);
            orderDto.Items = new List<OrderItemDto>();
            // custom after map code...
            foreach (var item in order.Items)
            {
                orderDto.Items.Add(new OrderItemDto()
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                });
            }
            return orderDto;
        }
    }
}
