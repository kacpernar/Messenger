namespace Messenger.Blazor.Services;

public class MessageHolder : IMessageHolder
{
    public List<Message> MessageList { get; set; } = new List<Message>();
}