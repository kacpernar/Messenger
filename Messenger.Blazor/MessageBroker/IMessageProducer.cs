namespace Messenger;

public interface IMessageProducer
{
    Task SendMessage (Message message);
}