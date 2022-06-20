using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace Messenger.Blazor.Services;

public class MessageHolder : IMessageHolder
{
    public MessageHolder(NavigationManager navigationManager, EventService eventService, IUserService userService, ILogger<MessageHolder> logger)
    {
        var hubConnection = new HubConnectionBuilder()
            .WithUrl("http://localhost/chatHub")
            .Build();

        hubConnection.On<Message>("ReceiveMessage", async message =>
        {
            if (message.UserName != userService.UserName)
            {
                switch (message.MessageStatus)
                {
                    case MessageStatus.DeletedToEveryone:
                    {
                        var messageToDelete = MessageList.Find(m => m.Id == message.Id);
                        if (messageToDelete != null)
                        {
                            await DeleteMessage(messageToDelete);
                        }

                        break;
                    }
                    case MessageStatus.DeletedByUser:
                        return;
                    case MessageStatus.None:
                        MessageList.Add(message);
                        break;
                    case MessageStatus.Pending:
                        MessageList.Add(message);
                        break;
                    case MessageStatus.StopPending:
                        MessageList.RemoveAll(t => t.UserName == message.UserName && t.MessageStatus == MessageStatus.Pending);
                        break;
                }
            }
            else
            {
                if(message.MessageStatus == MessageStatus.None)
                    MessageList.Add(message);
            }
            

            await eventService.OnMessage(this, EventArgs.Empty);
        });

        hubConnection.StartAsync();
    }
    
    public List<Message> MessageList { get; set; } = new();
    
    public Task DeleteMessage(Message message)
    {
        message.MessageStatus = MessageStatus.DeletedToEveryone;
        message.Text = $"Message was deleted by {message.UserName}";
        message.Source = string.Empty;
        return Task.CompletedTask;
    }
}