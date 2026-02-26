using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Interfaces.Services;

namespace SportsBicycleStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisputeController : ControllerBase
    {
        private readonly IDisputeService _disputeService;

        public DisputeController(IDisputeService disputeService)
        {
            _disputeService = disputeService;
        }

        /// <summary>
        /// Tạo tranh chấp mới (Buyer)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateDispute(DisputeDto disputeDto)
        {
            try
            {
                var dispute = await _disputeService.CreateDispute(disputeDto);
                return Ok(dispute);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Xem chi tiết tranh chấp theo ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDisputeById(string id)
        {
            var dispute = await _disputeService.GetDisputeByIdAsync(id);
            if (dispute == null)
                return NotFound(new { message = "Dispute not found" });
            return Ok(dispute);
        }

        /// <summary>
        /// Xem tranh chấp theo đơn hàng
        /// </summary>
        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetDisputeByOrderId(string orderId)
        {
            var dispute = await _disputeService.GetDisputeByOrderIdAsync(orderId);
            if (dispute == null)
                return NotFound(new { message = "No dispute found for this order" });
            return Ok(dispute);
        }

        /// <summary>
        /// Xem tất cả tranh chấp (Admin)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllDisputes()
        {
            var disputes = await _disputeService.GetAllDisputesAsync();
            return Ok(disputes);
        }

        /// <summary>
        /// Bổ sung bằng chứng cho tranh chấp (Buyer/Seller)
        /// </summary>
        [HttpPost("{id}/evidence")]
        public async Task<IActionResult> AddEvidence(string id, DisputeEvidenceDto evidenceDto)
        {
            try
            {
                evidenceDto.DisputeId = id;
                var evidence = await _disputeService.AddEvidence(evidenceDto);
                return Ok(evidence);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Xem danh sách bằng chứng của tranh chấp
        /// </summary>
        [HttpGet("{id}/evidence")]
        public async Task<IActionResult> GetEvidences(string id)
        {
            var evidences = await _disputeService.GetEvidencesByDisputeIdAsync(id);
            return Ok(evidences);
        }

        /// <summary>
        /// Admin xử lý kết quả tranh chấp
        /// </summary>
        [HttpPut("{id}/resolve")]
        public async Task<IActionResult> ResolveDispute(string id, ResolveDisputeDto resolveDto)
        {
            try
            {
                var dispute = await _disputeService.ResolveDispute(id, resolveDto);
                return Ok(dispute);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
