using Arta.Base.Core.ApiResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KitchenFlow.Application;
using KitchenFlow.Domain.Contract.Order;
using KitchenFlow.Domain.Order;
using KitchenFlow.Domain.Order.Mappers;
using KitchenFlow.Domain.Order.Validators;
using KitchenFlow.Presentation.Configs.ApiResults;
using KitchenFlow.Presentation.Validators;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

// Assuming the namespace of your project
namespace KitchenFlow.Presentation.Controllers.Orders
{
    public class OrderController : PresentationControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Creates a new order and returns 201 Created status.
        /// </summary>
        [HttpPost(Name = "AddOrder")]
        [Validator(typeof(OrderBasicValidator), "orderDto")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddOrder([FromBody] OrderDto orderDto)
        {
            // Execute business logic via Application Service
            var orderId = await _orderService.AddAsync(orderDto);

            // Return 201 Created with the location of the resource
            // Note: 'GetOrderById' is the name of GET endpoint method
            return CreatedAtAction(
                nameof(GetOrderById),
                new { id = orderId },
                ApiResult<GuidEntity>.Ok(new GuidEntity(orderId))
            );
        }

        /// <summary>
        /// Add new Items and returns 200 OK status.
        /// </summary>
        [HttpPost("{id}/add-items", Name = "AddOrderItems")]
        [Validator(typeof(OrderItemsValidator), "orderItemsDto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddOrderItems(Guid id, [FromBody] IEnumerable<OrderItemDto> orderItemsDto)
        {
            // Execute business logic via Application Service
            var order = await _orderService.AddItemsAsync(id, orderItemsDto);

            // Return 200 OK with the location of the resource
            return Ok(ApiResult<OrderDto>.Ok(order));
        }

        /// <summary>
        /// Update an order and returns 200 OK status.
        /// </summary>
        [HttpPatch("{id}/update", Name = "UpdateOrder")]
        [Validator(typeof(OrderBasicValidator), "orderDto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] UpdateOrderDto updateOrderDto)
        {
            // Execute business logic via Application Service
            var order = await _orderService.UpdateAsync(id, updateOrderDto);

            // Return 200 OK with the location of the resource
            return Ok(ApiResult<OrderDto>.Ok(order));
        }

        /// <summary>
        /// Delete an order and returns 200 OK status.
        /// </summary>
        [HttpDelete("{id}/delete", Name = "DeleteOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            // Execute business logic via Application Service
            await _orderService.DeleteAsync(id);

            // Return 200 OK with the location of the resource
            return NoContent();
        }

        /// <summary>
        /// Gets an order by Id.
        /// </summary>
        [HttpGet("{id}", Name = "GetOrderById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            // Gets domain/application DTO
            var order = await _orderService.GetAsync(id);

            // Here service throws NotFoundException when not found,
            // and middleware automatically returns 404.
            // So no need to check "order == null" here.

            return Ok(ApiResult<OrderDto>.Ok(order));
        }

        [HttpPost("{id}/accept", Name = "AcceptOrder")]
        public async Task<IActionResult> AcceptOrder(Guid id)
        {
            // Goes to next-state and gets domain/application DTO
            var order = await _orderService.AcceptOrderAsync(id);

            // Here service throws NotFoundException when not found,
            // and middleware automatically returns 404.
            // So no need to check "order == null" here.

            return Ok(ApiResult<OrderDto>.Ok(order));
        }

        [HttpPost("{id}/start-preparation", Name = "StartPreparationOrder")]
        public async Task<IActionResult> StartPreparationOrder(Guid id)
        {
            // Goes to next-state and gets domain/application DTO
            var order = await _orderService.StartPreparationAsync(id);

            // Here service throws NotFoundException when not found,
            // and middleware automatically returns 404.
            // So no need to check "order == null" here.

            return Ok(ApiResult<OrderDto>.Ok(order));
        }

        [HttpPost("{id}/finish-preparation", Name = "FinishPreparationOrder")]
        public async Task<IActionResult> FinishPreparationOrder(Guid id)
        {
            // Goes to next-state and gets domain/application DTO
            var order = await _orderService.FinishPreparationAsync(id);

            // Here service throws NotFoundException when not found,
            // and middleware automatically returns 404.
            // So no need to check "order == null" here.

            return Ok(ApiResult<OrderDto>.Ok(order));
        }

        [HttpPost("{id}/serve", Name = "ServeOrder")]
        public async Task<IActionResult> ServeOrder(Guid id)
        {
            // Goes to next-state and gets domain/application DTO
            var order = await _orderService.ServeAsync(id);

            // Here service throws NotFoundException when not found,
            // and middleware automatically returns 404.
            // So no need to check "order == null" here.

            return Ok(ApiResult<OrderDto>.Ok(order));
        }

        [HttpPost("{id}/close", Name = "CloseOrder")]
        public async Task<IActionResult> CloseOrder(Guid id)
        {
            // Goes to next-state and gets domain/application DTO
            var order = await _orderService.CloseAsync(id);

            // Here service throws NotFoundException when not found,
            // and middleware automatically returns 404.
            // So no need to check "order == null" here.

            return Ok(ApiResult<OrderDto>.Ok(order));
        }

        [HttpPost("{id}/cancel", Name = "CancelOrder")]
        public async Task<IActionResult> CancelOrder(Guid id)
        {
            // Goes to cancel-state and gets domain/application DTO
            var order = await _orderService.CancelAsync(id);

            // Here service throws NotFoundException when not found,
            // and middleware automatically returns 404.
            // So no need to check "order == null" here.

            return Ok(ApiResult<OrderDto>.Ok(order));
        }
    }
}
