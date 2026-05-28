using Restaurant.Domain.Contract.Order;

namespace Restaurant.Application
{
    public interface IOrderService 
    {
        public Task<Guid> AddAsync(OrderDto order);
        public Task<OrderDto> GetAsync(Guid id);
        public Task<OrderDto> AcceptOrderAsync(Guid id);
        public Task<OrderDto> StartPreparationAsync(Guid id);
        public Task<OrderDto> FinishPreparationAsync(Guid id);
        public Task<OrderDto> ServeAsync(Guid id);
        public Task<OrderDto> CloseAsync(Guid id);
        public Task<OrderDto> CancelAsync(Guid id);
    }
}
