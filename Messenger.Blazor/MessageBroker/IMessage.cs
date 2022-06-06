namespace Messenger;

public interface IMessage
{
    string? Text { get; set; }
    string UserName { get; set;}
    bool DeleteButtonsVisibility { get; set; }
    bool DeleteMessage { get; set; }
    string Source { get; set; }
}