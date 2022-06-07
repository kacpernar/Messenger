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
        
        
        return new MessageResponseModel();
    }
}