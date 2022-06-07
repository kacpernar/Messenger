using MediatR;

namespace Messenger.Blazor.Mediator;

public class MessageRequestModel : IRequest<MessageResponseModel>
{
    public Message Message { get; set; }
}