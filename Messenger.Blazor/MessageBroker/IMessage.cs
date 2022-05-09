namespace Messenger;

public interface IMessage
{
    string Text { get; set; }
    string UserName { get; set;}
}