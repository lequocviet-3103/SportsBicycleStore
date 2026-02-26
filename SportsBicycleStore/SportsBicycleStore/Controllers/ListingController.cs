using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Interfaces.Services;

namespace SportsBicycleStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingController : ControllerBase
    {
        private readonly IListingService _listingService;
        public ListingController(IListingService listingService)
        {
            _listingService = listingService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateListing(ListingDto listingDto)
        {
            var result = await _listingService.CreateListing(listingDto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var result = await _listingService.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound("Lising Id is not exist!!!");
            }
            return Ok(result);
        }

        [HttpPost("approve/{listingId}")]
        public async Task<IActionResult> ApproveListing(ApproveListingDto approveListingDto, string listingId)
        {
            var result = await _listingService.ApproveListing(approveListingDto, listingId);
            if (result == false)
            {
                return NotFound();
            }
            return Ok(result);
        }

           
    }
}
