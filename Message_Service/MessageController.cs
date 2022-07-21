using Message_Service.MessageBroker;
using Microsoft.AspNetCore.Mvc;

namespace Message_Service;

public class MessageController : Controller
{
	private readonly IMessageHolder _messageHolder;

	public MessageController(IMessageHolder messageHolder)
	{
		_messageHolder = messageHolder;
	}
	// GET
	public IActionResult Index()
	{
		try
		{
			var count = _messageHolder.MessageList.Count;
			var messages = new List<Message>();
			if (count > 10)
			{
				messages = _messageHolder.MessageList.GetRange(_messageHolder.MessageList.Count - 10, 10);

			}
			else
			{
				messages.AddRange(_messageHolder.MessageList);
			}
			return Ok(messages);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			Console.WriteLine(e);
			return BadRequest();
		}
		
	}
}