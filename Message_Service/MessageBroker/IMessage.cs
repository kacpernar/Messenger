namespace Message_Service.MessageBroker;

public interface IMessage
{
    string? Text { get; set; }
    string UserName { get; set;}
    bool DeleteButtonsVisibility { get; set; }
    bool DeleteMessage { get; set; }
    bool Deleted { get; set; }
    string Source { get; set; }
}