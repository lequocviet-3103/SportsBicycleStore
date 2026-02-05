using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Interfaces.Services;

namespace SportsBicycleStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMProductService _mProductService;
        public ProductController(IMProductService mProductService)
        {
            _mProductService = mProductService;
        }
        [HttpPost("create-bicycle")]
        public async Task<IActionResult> CreateBicycle([FromBody] ProductDto productDto)
        {
            var result = await _mProductService.CreateBicycle(productDto);
            return Ok(result);
        }
    }
}
