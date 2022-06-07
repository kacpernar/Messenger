using Messenger.Blazor;

namespace Messenger;

public interface IMessage
{
    string? Text { get; set; }
    string UserName { get; set; }
    bool DeleteButtonsVisibility { get; set; }
    MessageStatus MessageStatus { get; set; }
    string Source { get; set; }
}