namespace Messenger.Blazor.Services;

public interface IMessageHolder
{
    List<Message> MessageList { get; set; }
    Task DeleteMessage(Message message);
}