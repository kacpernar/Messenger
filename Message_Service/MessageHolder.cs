using Message_Service.MessageBroker;

namespace Message_Service;

public class MessageHolder : IMessageHolder
{
    public List<Message> MessageList { get; set; } = new List<Message>();

    public Task DeleteMessage(Message message)
    {
        MessageList.Remove(message);
        return Task.CompletedTask;
    } 
}