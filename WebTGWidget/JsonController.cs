using Microsoft.AspNetCore.Mvc;

namespace WebTGWidget
{
    [Route("api/[controller]")]
    [ApiController]
    public class JsonController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetJson()
        {
            var jsonResponse = new
            {
                Message = "Hello, World!",
                Status = "Success",
                Timestamp = DateTime.UtcNow
            };

            return Ok(jsonResponse);
        }
    }
}