namespace Messenger;

public interface IMessageProducer
{
    void SendMessage (Message message);
}