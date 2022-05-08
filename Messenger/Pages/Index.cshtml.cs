using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Messenger;

public class Index : PageModel
{
    private readonly IMessage _message;
    private readonly IMessageProducer _messageProducer;
    public string message { get; set; }
    [BindProperty]
    public string m { get; set; }
    
    public Index(IMessage message, IMessageProducer messageProducer)
    {
        _message = message;
        _messageProducer = messageProducer;
    }
    public void OnGet()
    {
        message = _message.Text;
    }
    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            _messageProducer.SendMessage(m);
            return Page();
        }
        return Page();
    }
}