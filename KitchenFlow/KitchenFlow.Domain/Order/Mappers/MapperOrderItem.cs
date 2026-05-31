using KitchenFlow.Domain.Contract.Order;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace KitchenFlow.Domain.Order.Mappers
{
    [Mapper]
    public partial class MapperOrderItem : IMapperOrderItem
    {
        private partial OrderItemDto MapToDto(OrderItem item);

        public OrderItemDto ToDto(OrderItem destination) => MapToDto(destination);

        public OrderItem ToEntity(OrderItemDto source)
        {
            var orderItem = new OrderItem(source.ProductId, source.Quantity, source.UnitPrice);

            return orderItem;
        }
    }

}
