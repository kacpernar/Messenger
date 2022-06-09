﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Messenger.Blazor.Services;

public class MessageHolder : IMessageHolder
{
    private readonly IUserService _userService;

    public MessageHolder(NavigationManager navigationManager, EventService eventService, IUserService userService)
    {
        _userService = userService;
        var hubConnection = new HubConnectionBuilder()
            .WithUrl(navigationManager.ToAbsoluteUri("/chathub"))
            .Build();

        var settings = MongoClientSettings.FromConnectionString("mongodb+srv://Jedi:lubiezapierdalac2115@messenger.og6bf.mongodb.net/?retryWrites=true&w=majority");
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        var client = new MongoClient(settings);
        var database = client.GetDatabase("Messenger");
        var collection = database.GetCollection<BsonDocument>("Messages");
        
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
                        var messageToDb=new BsonDocument {{"user", message.UserName },
                            {"text", message.Text},
                            {"imageSource", message.Source}
                        };
                        await collection.InsertOneAsync(messageToDb);
                        //MessageList.Add(message);
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
                {
                    var messageToDb = new BsonDocument {{"user", message.UserName },
                            {"text", message.Text},
                            {"imageSource", message.Source}
                        };
                    await collection.InsertOneAsync(messageToDb);
                }

                    //MessageList.Add(message);
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