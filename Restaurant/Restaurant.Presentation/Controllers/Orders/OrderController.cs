using Restaurant.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Arta.Base.Core.ApiResults;
using Restaurant.Application;
using Restaurant.Domain.Contract.Order;
using Restaurant.Domain.Order;
using Restaurant.Infrastructure.Commons;
using Restaurant.Presentation.Configs.ApiResults;

namespace Restaurant.Presentation.Controllers.Orders
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : PresentationControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapperOrder _mapper;

        public OrderController(IOrderService orderService, IMapperOrder mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }


        [HttpPost(Name = "AddOrder")]
        [Validator(ValidatorType_Order.Basic, "orderDto")]
        public async Task<IActionResult> AddOrder(OrderDto orderDto)
        {
            var order = _mapper.CustomMapOrderDtoToOrder(orderDto);

            var result = await _orderService.AddAsync(order);

            return Ok(ApiResult<GuidEntity>.Ok(new GuidEntity(result)));
        }
    }
}
