

namespace Messenger.Blazor.Services;

public class MessageService : IMessageService
{
    private readonly IMessageHolder _messageHolder;
    private readonly IMessageProducer _messageProducer;

    public MessageService(IMessageHolder messageHolder,IMessageProducer messageProducer)
    {
        _messageHolder = messageHolder;
        _messageProducer = messageProducer;
    }
    public async Task DeleteMessageToEveryone(Message message)
    {
        await _messageHolder.DeleteMessage(message);
        await _messageProducer.SendMessage(new Message()
        {
            Id = message.Id,
            MessageStatus = MessageStatus.DeletedToEveryone
        });
    }
    public async Task DeleteMessageByUser(Message message)
    {
        await _messageHolder.DeleteMessage(message);
        await _messageProducer.SendMessage(new Message()
        {
            Id = message.Id,
            MessageStatus = MessageStatus.DeletedByUser
        });
    }
}