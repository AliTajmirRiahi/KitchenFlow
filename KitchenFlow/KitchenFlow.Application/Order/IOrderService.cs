using KitchenFlow.Domain.Contract.Order;

namespace KitchenFlow.Application
{
    public interface IOrderService
    {
        public Task<Guid> AddAsync(OrderDto order);
        public Task<OrderDto> UpdateAsync(Guid id, UpdateOrderDto updateOrderDto);
        public Task DeleteAsync(Guid id);
        public Task<OrderDto> GetAsync(Guid id);
        public Task<OrderDto> AcceptOrderAsync(Guid id);
        public Task<OrderDto> StartPreparationAsync(Guid id);
        public Task<OrderDto> FinishPreparationAsync(Guid id);
        public Task<OrderDto> ServeAsync(Guid id);
        public Task<OrderDto> CloseAsync(Guid id);
        public Task<OrderDto> CancelAsync(Guid id);
        public Task<OrderDto> AddItemsAsync(Guid id, IEnumerable<OrderItemDto> orderItemsDto);
    }
}
