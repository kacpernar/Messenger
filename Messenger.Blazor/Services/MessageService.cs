using MediatR;
using Messenger.Blazor.Mediator;

namespace Messenger.Blazor.Services;

public class MessageService : IMessageService
{
    private readonly IMediator _mediator;

    public MessageService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task UpdateMessagesList(Message message)
    {
        try
        {
            var result = await _mediator.Send(new MessageRequestModel()
            {
                Message = message
            });

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}