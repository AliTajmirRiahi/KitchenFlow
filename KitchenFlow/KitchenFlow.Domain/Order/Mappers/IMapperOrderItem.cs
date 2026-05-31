using Arta.Domain.Core.Commons;
using Restaurant.Domain.Contract.Order;

namespace Restaurant.Domain.Order.Mappers
{
    // Inherits from the generic interface
    public interface IMapperOrderItem : IMapper<OrderItemDto, OrderItem>
    {
        
    }
}
