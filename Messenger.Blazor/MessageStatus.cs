namespace Messenger.Blazor;

public enum MessageStatus
{
    None = 0,
    DeletedByUser = 1,
    DeletedToEveryone = 2,
    Pending = 4,
    StopPending = 5
}