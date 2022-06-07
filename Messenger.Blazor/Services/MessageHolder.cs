using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace Messenger.Blazor.Services;

public class MessageHolder : IMessageHolder
{
    public MessageHolder(NavigationManager navigationManager, EventService eventService)
    {
        var hubConnection = new HubConnectionBuilder()
            .WithUrl(navigationManager.ToAbsoluteUri("/chathub"))
            .Build();

        hubConnection.On<Message>("ReceiveMessage", async message =>
        {
            if (message.DeleteMessage)
            {
                var messageToDelete = MessageList.Find(m => m.Id == message.Id);
                if (messageToDelete != null)
                {
                    await DeleteMessage(messageToDelete);
                }
            }
            else
            {
                MessageList.Add(message);
            }
            await eventService.OnMessage(this, EventArgs.Empty);
        });

        hubConnection.StartAsync();
    }
    public List<IUser> Users { get; set; } = new List<IUser>();
    public List<Message> MessageList { get; set; } = new List<Message>();

    public Task DeleteMessage(Message message)
    {
        MessageList.Remove(message);
        return Task.CompletedTask;
    } 
}