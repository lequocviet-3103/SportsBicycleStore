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

        [Authorize(Roles = "1, 2")]
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

        [Authorize(Roles = "1, 2, 3, 4")]
        [HttpGet("get-all-product")]
        public async Task<IActionResult> GetAllProduct()
        {
            var result = await _mProductService.GetAllProductDtos();
            return Ok(result);
        }

        [Authorize(Roles = "1, 2, 3")]
        [HttpGet("get-product-detail-by-productId/{productId}")]
        public async Task<IActionResult> GetProductById(string productId)
        {
            var result = await _mProductService.GetProductById(productId);
            if (result == null)
            {
                return NotFound("Product not found.");
            }
            return Ok(result);
        }

        [Authorize(Roles = "1, 2")]
        [HttpGet("get-product-by-seller/{sellerId}")]
        public async Task<IActionResult> GetProductBySellerId(string sellerId)
        {
            var result = await _mProductService.GetProductBySellerId(sellerId);
            if (result == null)
            {
                return NotFound("Product not found.");
            }
            return Ok(result);
        }
        [Authorize(Roles = "1, 2, 3, 4")]
        [HttpGet("get-product-by-product-id")]
        public async Task<IActionResult> GetProductByProductId(string productId)
        {
            var result = await _mProductService.GetMproductByIdAsync(productId);
            if (result == null)
            {
                return NotFound("Product not found.");
            }
            return Ok(result);
        }
    }
}
