using Arta.Domain.Core.Commons.Enums;
using Restaurant.Domain.Contract.Order;
using Restaurant.Domain.Order.Mappers;
using Riok.Mapperly.Abstractions;

namespace Restaurant.Domain.Order.Mappers
{
    [Mapper]
    public partial class MapperOrder : IMapperOrder
    {
        // Auto-generated mapping for simple properties
        private partial OrderDto MapToDto(Order order);

        private readonly IMapperOrderItem _itemMapper;

        public MapperOrder(IMapperOrderItem itemMapper) 
        {
            _itemMapper = itemMapper;
        }

        public OrderDto ToDto(Order order)
        {
            //1. Create a Order Dto 
            var dto = MapToDto(order);

            //2.Map Items Dto 
            dto.Items = [.. order.Items.Select(_itemMapper.ToDto)];

            //3.Other map
            dto.OrderStatusDescription = order.Status.ToDescription();

            return dto;
        }

        public Order ToEntity(OrderDto dto)
        {
            // 1. Create the aggregate root
            var order = new Order(dto.CustomerId, dto.TableId);

            // 2. Map items using a dedicated mapper or logic
            foreach (var itemDto in dto.Items)
            {
                order.AddItem(itemDto.ProductId, itemDto.Quantity, itemDto.UnitPrice);
            }

            return order;
        }
    }
}
