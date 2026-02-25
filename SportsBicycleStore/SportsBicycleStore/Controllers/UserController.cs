using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Interfaces.Services;
using SportsBicycleStore.Application.SearchFilter;

namespace SportsBicycleStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "1,2")]
        [HttpGet("get-user-by-search-filter")]
        public async Task<IActionResult> GetUsers([FromQuery] UserSearchFilter filter)
        {
            var users = await _userService.GetUsersAsync(filter);
            return Ok(users);
        }

        [Authorize(Roles = "1,2, 3")]
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] UpdateUserDto dto)
        {
            var updatedUser = await _userService.UpdateUserAsync(userId, dto);

            if (updatedUser == null)
            {
                return NotFound("User not found.");
            }

            return Ok(updatedUser);
        }

        [Authorize(Roles = "1,2")]
        [HttpPatch("{userId}/UpdateStatus")]
        public async Task<IActionResult> SoftDeleteUser(string userId, [FromBody] SoftDeleteUserDto dto)
        {
            var result = await _userService.SoftDeleteUserAsync(userId, dto);

            if (!result)
            {
                return NotFound("User not found.");
            }

            return NoContent();
        }
    }
}
