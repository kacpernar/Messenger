using MediatR;
using Messenger.Blazor.Services;

namespace Messenger.Blazor.Mediator;

public class MessageHandler : IRequestHandler<MessageRequestModel, MessageResponseModel>
{
    private readonly IMessageHolder _messageHolder;
    private readonly EventService _eventService;

    public MessageHandler(IMessageHolder messageHolder, EventService eventService)
    {
        _messageHolder = messageHolder;
        _eventService = eventService;
    }
    public async Task<MessageResponseModel> Handle(MessageRequestModel request, CancellationToken cancellationToken)
    {
        var message = request.Message;
        if (message.DeleteMessage)
        {
            var messageToDelete = _messageHolder.MessageList.Find(m => m.Id == message.Id);
            if (messageToDelete != null)
            {
                await _messageHolder.DeleteMessage(messageToDelete);
            }
        }
        else
        {
            _messageHolder.MessageList.Add(message);
        }

        await _eventService.OnMessage(this, EventArgs.Empty);
        return new MessageResponseModel();
    }
}