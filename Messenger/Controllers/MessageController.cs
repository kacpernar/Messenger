using Microsoft.AspNetCore.Mvc;

namespace Messenger
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpPost]
        public IActionResult SendMessage(string message)
        {
            try
            {
                return Ok(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }
    }
}