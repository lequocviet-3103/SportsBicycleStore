using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Interfaces.Services;

namespace SportsBicycleStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMCategoryService _categoryService;

        public CategoryController(IMCategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [Authorize(Roles = "1, 2")]
        [HttpPost("createCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto dto)
        {
            var result = await _categoryService.CreateCategoryAsync(dto);
            if (result == null)
            {
                return BadRequest("Failed to create category.");
            }
            return Ok(result);
        }

        [Authorize(Roles = "1, 2")]
        [HttpPut("{categoryId}/updateCategory")]
        public async Task<IActionResult> UpdateCategory(string categoryId, [FromBody] CategoryDto dto)
        {
            var updatedCategory = await _categoryService.UpdateCategoryAsync(categoryId, dto);

            if (updatedCategory == null)
            {
                return NotFound("Category not found.");
            }

            return Ok(updatedCategory);
        }

        [Authorize(Roles = "1, 2")]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            if (categories == null || !categories.Any())
            {
                return NotFound("No categories found.");
            }

            return Ok(categories);
        }
    }
}