using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Interfaces.Services;

namespace SportsBicycleStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMOrderService _orderService;
        public OrderController(IMOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize("1, 2, 3")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDto orderDto)
        {
            var order = await _orderService.CreateOrder(orderDto);
            return Ok(order);
        }

        [Authorize("1, 2")]
        [HttpGet("get-all-order")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrderDtos();
            return Ok(orders);
        }

        [Authorize("1, 2, 3")]
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(string orderId)
        {
            var order = await _orderService.GetOrderById(orderId);
            if (order == null)
            {
                return NotFound("Order not found.");
            }
            return Ok(order);
        }

         [Authorize("1, 2")]
         [HttpGet("get-order-by-user/{userId}")]
         public async Task<IActionResult> GetOrdersByUserId(string userId)
         {
             var orders = await _orderService.GetOrderByUserId(userId);
             return Ok(orders);
        }

        [Authorize("1, 2, 3")]
        [HttpPut("get-order-by-order-id{orderId}")]
        public async Task<IActionResult> GetOrderByOrderId(string orderId) 
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return NotFound("Order not found.");
            }
            return Ok(order);
        }
    }
}
