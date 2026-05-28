using Arta.Application.Core;
using Arta.Base.Core.Exceptions;
using Restaurant.Domain.Contract.Order;
using Restaurant.Domain.Order;
using Restaurant.Domain.Order.Mappers;
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
        private readonly IMapperOrder _mapper;

        public OrderService(IOrderRepository orderRepository, IMapperOrder mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }


        public async Task<Guid> AddAsync(OrderDto orderDto)
        {
            // Map DTO to Domain Entity
            var order = _mapper.ToEntity(orderDto);

            return await _orderRepository.AddAsync(order);
        }
        private async Task<Order> GetOrderOrThrowAsync(Guid id)
        {
            // Load aggregate (with Items thanks to Include)
            var order = await _orderRepository.GetByIdAsync(id);

            //Check existance of order
            if (order == null)
                throw new NotFoundException($"Order with id {id} was not found.", "OrderNotFoundError");

            return order;
        }

        public async Task<OrderDto> GetAsync(Guid id)
        {
            var order = await GetOrderOrThrowAsync(id);

            return _mapper.ToDto(order);
        }


        public async Task<OrderDto> AcceptOrderAsync(Guid id)
        {
            var order = await GetOrderOrThrowAsync(id);

            // Domain-driven state transition
            order.Accept();

            // Repository handles SaveChanges internally
            await _orderRepository.UpdateAsync(order);

            return _mapper.ToDto(order);
        }

        public async Task<OrderDto> StartPreparationAsync(Guid id)
        {
            var order = await GetOrderOrThrowAsync(id);

            // Domain-driven state transition
            order.StartPreparation();

            // Repository handles SaveChanges internally
            await _orderRepository.UpdateAsync(order);

            return _mapper.ToDto(order);
        }

        public async Task<OrderDto> FinishPreparationAsync(Guid id)
        {
            var order = await GetOrderOrThrowAsync(id);

            // Domain-driven state transition
            order.FinishPreparation();

            // Repository handles SaveChanges internally
            await _orderRepository.UpdateAsync(order);

            return _mapper.ToDto(order);
        }

        public async Task<OrderDto> ServeAsync(Guid id)
        {
            var order = await GetOrderOrThrowAsync(id);

            // Domain-driven state transition
            order.Serve();

            // Repository handles SaveChanges internally
            await _orderRepository.UpdateAsync(order);

            return _mapper.ToDto(order);
        }

        public async Task<OrderDto> CloseAsync(Guid id)
        {
            var order = await GetOrderOrThrowAsync(id);

            // Domain-driven state transition
            order.Close();

            // Repository handles SaveChanges internally
            await _orderRepository.UpdateAsync(order);

            return _mapper.ToDto(order);
        }

        public async Task<OrderDto> CancelAsync(Guid id)
        {
            var order = await GetOrderOrThrowAsync(id);

            // Domain-driven state transition
            order.Cancel();

            // Repository handles SaveChanges internally
            await _orderRepository.UpdateAsync(order);

            return _mapper.ToDto(order);
        }
    }
}
