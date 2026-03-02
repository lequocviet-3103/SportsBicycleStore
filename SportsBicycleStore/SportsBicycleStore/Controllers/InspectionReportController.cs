using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Interfaces.Services;

namespace SportsBicycleStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InspectionReportController : ControllerBase
    {
        private readonly IMInspectionReportService _inspectionReportService;
        public InspectionReportController(IMInspectionReportService inspectionReportService)
        {
            _inspectionReportService = inspectionReportService;
        }

        [Authorize(Roles = "1,4")]
        [HttpPost("create-inspector-report")]
        public async Task<IActionResult> CreateInspectionReport([FromBody] InspectionReportDto inspectionReportDto)
        {
            var result = await _inspectionReportService.CreateInspectionReport(inspectionReportDto);
            return Ok(result);
        }

        [Authorize(Roles = "1,4")]
        [HttpPost]
        [Route("update/{reportId}")]
        public async Task<IActionResult> UpdateInspectionReport([FromBody] UpdateInspectionReportDto updateInspectionReportDto, [FromRoute] string reportId)
        {
            var isUpdated = await _inspectionReportService.UpdateInspectionReport(updateInspectionReportDto, reportId);
            if (!isUpdated)
            {
                return NotFound("Inspection report not found.");
            }
            return Ok("Inspection report updated successfully.");
        }


        [Authorize(Roles = "1, 2, 4")]
        [HttpGet("get-all-inspector-report")]
        public async Task<IActionResult> GetAllInspectionReports()
        {
            var reports = await _inspectionReportService.GetAllInspectionReportsAsync();
            return Ok(reports);
        }

        [Authorize(Roles = "1, 2, 4")]
        [HttpGet("get-all-inspector-report-product-name")]
        public async Task<IActionResult> GetAllInspectionReportProductName()
        {
            var reports = await _inspectionReportService.GetAllInspectionReportProductName();
            return Ok(reports);
        }

        [Authorize(Roles = "1, 2, 4")]
        [HttpGet("get-all-inspector-report-by-Id/{reportId}")]
        public async Task<IActionResult> GetInpectionReportByReportId(string reportId)
        {
            var report = await _inspectionReportService.GetInpectionReportByReportId(reportId);
            if (report == null)
            {
                return NotFound("Inspection report not found.");
            }
            return Ok(report);

        }
    }
}
