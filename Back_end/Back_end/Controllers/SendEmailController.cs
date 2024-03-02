using BLL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendEmailController : ControllerBase
    {
        private ISendEmailBus _SendEmailBusBus;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public SendEmailController(ISendEmailBus SendEmailBusBus, IWebHostEnvironment hostingEnvironment)
        {
            _SendEmailBusBus = SendEmailBusBus;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail([FromForm]string email, [FromForm] string donhang)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "Temlate_Email", "index.html");

            // Đọc nội dung từ tệp HTML
            string htmlMessage = await System.IO.File.ReadAllTextAsync(htmlFilePath);
            htmlMessage = htmlMessage.Replace("{{DonHang}}", donhang);

            // Gửi email
            await _SendEmailBusBus.SendEmailAsync(email, "ACHINO", htmlMessage);

            return Ok("Email sent successfully!");
        }

    }
}
