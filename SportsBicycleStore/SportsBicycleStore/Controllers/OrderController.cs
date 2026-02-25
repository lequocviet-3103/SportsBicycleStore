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

        [Authorize(Roles = "1,3")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDto orderDto)
        {
            var order = await _orderService.CreateOrder(orderDto);
            return Ok(order);
        }
    }
}
