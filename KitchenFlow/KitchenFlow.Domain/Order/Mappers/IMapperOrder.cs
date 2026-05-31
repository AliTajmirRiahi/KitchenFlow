using Arta.Domain.Core.Commons;
using Restaurant.Domain.Contract.Order;

namespace Restaurant.Domain.Order.Mappers
{
    // Inherits from the generic interface
    public interface IMapperOrder : IMapper<OrderDto, Order>
    {
        
    }
}
