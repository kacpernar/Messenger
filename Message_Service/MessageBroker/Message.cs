namespace Message_Service.MessageBroker;

public class Message : IMessage
{
    public string? Text { get; set; }
    public string UserName { get; set; }
    public string Source { get; set; }
    public bool DeleteButtonsVisibility { get; set; }
    public MessageStatus MessageStatus { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
}