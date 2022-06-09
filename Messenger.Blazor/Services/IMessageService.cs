namespace Messenger.Blazor.Services;

public interface IMessageService
{
    Task DeleteMessageToEveryone(Message message);
    Task DeleteMessageByUser(Message message);
}