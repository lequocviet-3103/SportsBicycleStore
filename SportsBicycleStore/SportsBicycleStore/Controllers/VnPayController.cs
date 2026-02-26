using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Interfaces.Services;
using System.Threading.Tasks;

namespace SportsBicycleStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VnPayController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;
        public VnPayController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

        [HttpPost]
        public IActionResult CreatePaymentUrlVnpay(PaymentInformationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
            return Ok(new { PaymentUrl = url });
        }
        [HttpGet("Callback")]
        public IActionResult PaymentCallbackVnpay()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            return new JsonResult(response);
        }

       /** [HttpPost("create-payment")]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentDto paymentDto)
        {
            var payment = await _vnPayService.CreatePayment(paymentDto);
            return Ok(payment);
        }*/
    }
}
