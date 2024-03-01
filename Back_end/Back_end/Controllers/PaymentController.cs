using BUS.Interface;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Model;

namespace Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        public readonly IPaymentBus _pay;

        public PaymentController(IPaymentBus pay)
        {
            _pay = pay;
        }

        [HttpPost("createURL")]
        public async Task<ActionResult<string>> createURL([FromBody] PaymentDto dto)
        {
            var createdEntity = await _pay.CreateURL(dto);

            return Ok(createdEntity);
        }
        [HttpGet("Callback")]
        public async Task<IActionResult> PaymentCallback()
        {
            // Lấy chuỗi truy vấn từ yêu cầu HTTP
            var queryString = Request.QueryString.ToString();
            var result = await _pay.GetPaymentResponse(queryString);
            // Trả về kết quả thành công
            return Ok(result);
        }
    }
}
