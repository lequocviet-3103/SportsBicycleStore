using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "1,2")]
        [HttpPost("create-bicycle")]
        public async Task<IActionResult> CreateBicycle([FromBody] ProductDto productDto)
        {
            var result = await _mProductService.CreateBicycle(productDto);
            return Ok(result);
        }


        [Authorize(Roles = "1,2")]
        [HttpPut("{productId}/UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(string productId, [FromBody] UpdateProductDto dto)
        {
            var updatedProduct = await _mProductService.UpdateBicycleAsync(productId, dto);

            if (updatedProduct == null)
            {
                return NotFound("Product not found.");
            }

            return Ok(updatedProduct);
        }
    }
}
