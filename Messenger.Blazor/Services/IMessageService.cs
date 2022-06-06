namespace Messenger.Blazor.Services;

public interface IMessageService
{
    Task UpdateMessagesList(Message message);
}