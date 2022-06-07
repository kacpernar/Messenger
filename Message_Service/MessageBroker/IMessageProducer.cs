namespace Message_Service.MessageBroker;

public interface IMessageProducer
{
    void SendMessage (Message message);
}