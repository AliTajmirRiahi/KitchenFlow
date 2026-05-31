using Arta.Domain.Core.Commons;
using KitchenFlow.Domain.Contract.Order;

namespace KitchenFlow.Domain.Order.Mappers
{
    // Inherits from the generic interface
    public interface IMapperOrder : IMapper<OrderDto, Order>
    {
        
    }
}
