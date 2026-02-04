using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsBicycleStore.Domain.Enum;
using SportsBicycleStore.Exceptions;
using SportsBicycleStore.Infastructure.Data.Models;

namespace SportsBicycleStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WeatherForecastController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var roles = await _context.Roles.ToListAsync();

                if (!roles.Any())
                    return NotFound("No roles found.");

                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error: {ex.Message}");
            }
        }
    }
}
