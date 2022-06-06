namespace Messenger.Blazor.Services;

public class MessageHolder : IMessageHolder
{
    public List<Message> MessageList { get; set; } = new List<Message>();

    public Task DeleteMessage(Message message)
    {
        //.DeleteButtonsVisibility = false;
        message.Deleted = true;
        message.Text = $"Message was deleted by {message.UserName}";
        message.Source = string.Empty;
        return Task.CompletedTask;
    } 
}