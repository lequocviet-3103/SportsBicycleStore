using Microsoft.AspNetCore.Mvc;
using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Interfaces.Services;
using SportsBicycleStore.Infastructure.Services;

namespace SportsBicycleStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IMBrandService _brandService;
        public BrandController(IMBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBrand([FromBody] BrandDto dto)
        {
            var result = await _brandService.CreateBrandAsync(dto);
            if (result == null)
            {
                return BadRequest("Failed to create brand.");
            }
            return Ok(result);
        }

        [HttpPut("{brandId}/updateBrand")]
        public async Task<IActionResult> UpdateBrand(string brandId, [FromBody] BrandDto dto)
        {
            var result = await _brandService.UpdateBrandAsync(brandId, dto);

            if (result == null)
            {
                return NotFound("Brand not found.");
            }

            return Ok(result);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllBrands()
        {
            var brands = await _brandService.GetAllBrandsAsync();

            if (brands == null || !brands.Any())
            {
                return NotFound("No brands found.");
            }

            return Ok(brands);
        }

    }
}
