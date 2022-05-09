namespace Messenger.Blazor.Services;

public class MessageHolder : IMessageHolder
{
    public List<Message> MessageList { get; set; } = new List<Message>();

    public Task DeleteMessage(Message message)
    {
        MessageList.Remove(message);
        return Task.CompletedTask;
    } 
}