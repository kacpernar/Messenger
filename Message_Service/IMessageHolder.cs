using Message_Service.MessageBroker;

namespace Message_Service;

public interface IMessageHolder
{
    List<Message> MessageList { get; set; }
    Task DeleteMessage(Message message);
}