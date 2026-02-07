using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("get-user-by-search-filter")]
        public async Task<IActionResult> GetUsers([FromQuery] UserSearchFilter filter)
        {
            var users = await _userService.GetUsersAsync(filter);
            return Ok(users);
        }
    }
}
