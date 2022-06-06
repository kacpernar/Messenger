﻿namespace Messenger.Blazor.Services;

public interface IMessageHolder
{
    public List<IUser> Users { get; set; } 
    List<Message> MessageList { get; set; }
    Task DeleteMessage(Message message);
}