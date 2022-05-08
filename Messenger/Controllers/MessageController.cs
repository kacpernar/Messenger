using Microsoft.AspNetCore.Mvc;

namespace Messenger
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageProducer _messageProducer;

        public MessageController(IMessageProducer messageProducer)
        {
            _messageProducer = messageProducer;
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage(string message)
        {
            try
            {
                _messageProducer.SendMessage(message);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }
    }
}